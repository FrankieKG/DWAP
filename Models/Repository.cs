using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication5.Models;

public class Repository : IRepository
{

    private readonly ApplicationDbContext context;
    
    public Repository(ApplicationDbContext context)
    {
      this.context = context;
    }

    
    public IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations => context.ApplicationAndEvaluations;
    public IQueryable<Organization> Organizations => context.Organizations;
    public IQueryable<Participant> Participants => context.Participants;
    public IQueryable<Payment> Payments => context.Payments;
    public IQueryable<PreviousApplication> PreviousApplications => context.PreviousApplications;
    public IQueryable<Program> Programs => context.Programs;
    public IQueryable<ReportAndReclaim> ReportAndReclaims => context.ReportAndReclaims;
    public IQueryable<ScholarshipAndGrant> ScholarshipAndGrants => context.ScholarshipAndGrants;


    public List<ApplicationAndEvaluation> ApplicationAndEvaluation { get; set; }
    public List<Organization> Organization { get; set; }
    public List<Participant> Participant { get; set; }
    public List<Payment> Payment { get; set; }
    public List<PreviousApplication> PreviousApplication { get; set; }
    public List<Program> Program { get; set; }
    public List<ReportAndReclaim> ReportAndReclaim { get; set; }
    public List<ScholarshipAndGrant> ScholarshipAndGrant { get; set; }


    //Läser data från en Excel-fil och konverterar den till fält i POCO-klasser:
    public async Task ReadFile(IFormFile file)
    {

        //Läser in en nedsparad JSON-sträng med Dictionary-data:
        string jsonString = File.ReadAllText("dictionary.json");

        //Packar upp JSON-strängen till ett Dictionary:
        Dictionary<string, (string, Type)> columnMappings = JsonConvert.DeserializeObject<Dictionary<string, (string, Type)>>(jsonString);

        await ExcelImporter(file, columnMappings);
    }


    private async Task ExcelImporter(IFormFile file, Dictionary<string, (string, Type)> columnMappings)
    {
        Dictionary<Type, object> modelInstances = new Dictionary<Type, object>
        {
            { typeof(ApplicationAndEvaluation), new ApplicationAndEvaluation() },
            { typeof(Organization), new Organization() },
            { typeof(Participant), new Participant() },
            { typeof(Payment), new Payment() },
            { typeof(PreviousApplication), new PreviousApplication() },
            { typeof(Program), new Program() },
            { typeof(ReportAndReclaim), new ReportAndReclaim() },
            { typeof(ScholarshipAndGrant), new ScholarshipAndGrant() }
        };

        ApplicationAndEvaluation = new List<ApplicationAndEvaluation>();
        Organization = new List<Organization>();
        Participant = new List<Participant>();
        Payment = new List<Payment>();
        PreviousApplication = new List<PreviousApplication>();
        Program = new List<Program>();
        ReportAndReclaim = new List<ReportAndReclaim>();
        ScholarshipAndGrant = new List<ScholarshipAndGrant>();

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Matchar kolumnnamn i Excelfilen mot Dictionary-values och returnerar en lista med rätt
                // kolumnnamn för tabellerna i databasen:
                Dictionary<string, Type> HeaderProperties = GetHeaderProperties(worksheet, columnMappings);



                for (int row = 2; row <= rowCount; row++) // Börjar på rad 2 för att hoppa över rubrikerna
                {
                    ApplicationAndEvaluation applicationAndEvaluations = new ApplicationAndEvaluation();
                    Organization organizations = new Organization();
                    Participant participants = new Participant();
                    Payment payments = new Payment();
                    PreviousApplication previousApplications = new PreviousApplication();
                    Program programs = new Program();
                    ReportAndReclaim reportAndReclaims = new ReportAndReclaim();
                    ScholarshipAndGrant scholarshipandgrants = new ScholarshipAndGrant();


                    //Snabb felkontroll för att vara säker på att kolumnerna i Excel finns i Dictionaryt:
                    CheckIfNoOfColumnsMatch(HeaderProperties, colCount);


                    for (int col = 1; col <= colCount; col++)
                    {
                        //Kolumnnamnet i databasen som ska skrivas till:
                        var colName = HeaderProperties.ElementAt(col-1).Key.ToString();

                        //Datat i den aktuella cellen:
                        var cellValue = worksheet.Cells[row, col].Value;
                        string cellData = cellValue != null ? cellValue.ToString() : string.Empty;


                        foreach (var model in modelInstances)
                        {
                            var modelType = model.Key;
                            var modelInstance = model.Value;
                            
                            if (HeaderProperties.ElementAt(col - 1).Value == modelType)
                            {

                                var prop = modelType.GetProperty(colName);

                                if (prop != null)
                                {
                                    var value = Convert.ChangeType(cellData, prop.PropertyType);
                                    prop.SetValue(modelInstance, value);
                                }
                            }
                        }
                    }
                    ApplicationAndEvaluation.Add(applicationAndEvaluations);
                    Organization.Add(organizations);
                    Participant.Add(participants);
                    Payment.Add(payments);
                    PreviousApplication.Add(previousApplications);
                    Program.Add(programs);
                    ReportAndReclaim.Add(reportAndReclaims);
                    ScholarshipAndGrant.Add(scholarshipandgrants);
                }
            }
            context.ApplicationAndEvaluations.AddRange(ApplicationAndEvaluation);
            context.Organizations.AddRange(Organization);
            context.Participants.AddRange(Participant);
            context.Payments.AddRange(Payment);
            context.PreviousApplications.AddRange(PreviousApplication);
            context.Programs.AddRange(Program);
            context.ReportAndReclaims.AddRange(ReportAndReclaim);
            context.ScholarshipAndGrants.AddRange(ScholarshipAndGrant);

            context.SaveChanges();
        }
    }


    private object ConvertCellValue(string cellData, Type targetType)
    {
        if (string.IsNullOrEmpty(cellData))
        {
            return null;
        }

        switch (targetType.Name)
        {
            case "Int32":
                if (int.TryParse(cellData, out int intValue))
                {
                    return intValue;
                }
                break;
            case "Double":
                if (double.TryParse(cellData, out double doubleValue))
                {
                    return doubleValue;
                }
                break;
            case "DateTime":
                if (DateTime.TryParse(cellData, out DateTime dateTimeValue))
                {
                    return dateTimeValue;
                }
                break;
            case "String":
                return cellData;
        }
        return null;
    }


    private void CheckIfNoOfColumnsMatch(Dictionary<string, Type> HeaderProperties, int colCount)
    {
        if (HeaderProperties.Count < colCount)
        {
            // Felmeddelande:
            throw new Exception($"HeaderProperties has {HeaderProperties.Count} items, but col is {colCount}");
        }
    }


    private Dictionary<string, Type> GetHeaderProperties(ExcelWorksheet worksheet, Dictionary<string, (string, Type)> columnMappings)
    {
        ExcelRange headerCells = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
        List<string> columnNames = headerCells.Select(cell => cell.Value?.ToString().Trim()).ToList();

        Dictionary<string, Type> headerProperties = new();
        foreach (string columnName in columnNames)
        {
            if (columnMappings.TryGetValue(columnName, out var mapping))
            {
                headerProperties[mapping.Item1] = mapping.Item2;
            }
            else
            {
                throw new Exception("Column " + columnName + " mismatch");
            }
        }

        return headerProperties;
    }




    public void GenerateNewDictionary()
    {
        DictionaryGeneration dictionary = new();
    }


}