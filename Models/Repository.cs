using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Collections;

namespace WebApplication5.Models;

public class Repository : IRepository
{
    
    public Repository(ApplicationDbContext context)
    {
      this.context = context;
    }

    private readonly ApplicationDbContext context;

    public IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations => context.ApplicationAndEvaluations;
    public IQueryable<Organization> Organizations => context.Organizations;
    public IQueryable<Participant> Participants => context.Participants;
    public IQueryable<Payment> Payments => context.Payments;
    public IQueryable<PreviousApplication> PreviousApplications => context.PreviousApplications;
    public IQueryable<Program> Programs => context.Programs;
    public IQueryable<ReportAndReclaim> ReportAndReclaims => context.ReportAndReclaims;
    public IQueryable<ScholarshipAndGrant> ScholarshipAndGrants => context.ScholarshipAndGrants;

    public List<ApplicationAndEvaluation> ApplicationAndEvaluationList { get; set; }
    public List<Organization> OrganizationList { get; set; }
    public List<Participant> ParticipantList { get; set; }
    public List<Payment> PaymentList { get; set; }
    public List<PreviousApplication> PreviousApplicationList { get; set; }
    public List<Program> ProgramList { get; set; }
    public List<ReportAndReclaim> ReportAndReclaimList { get; set; }
    public List<ScholarshipAndGrant> ScholarshipAndGrantList { get; set; }


    //Läser data från en Excel-fil och konverterar den till fält i POCO-klasser:
    public async Task ReadFile(IFormFile file)
    {

        //Läser in en nedsparad JSON-sträng med Dictionary-data:
        string jsonString = File.ReadAllText("columnMappings.json");

        //Packar upp JSON-strängen till ett Dictionary:
        Dictionary<string, (string, Type)> columnMappings = JsonConvert.DeserializeObject<Dictionary<string, (string, Type)>>(jsonString);

        await ExcelImporter(file, columnMappings);
    }


    private async Task ExcelImporter(IFormFile file, Dictionary<string, (string, Type)> columnMappings)
    {
        
        ApplicationAndEvaluationList = new List<ApplicationAndEvaluation>();
        OrganizationList = new List<Organization>();
        ParticipantList = new List<Participant>();
        PaymentList = new List<Payment>();
        PreviousApplicationList = new List<PreviousApplication>();
        ProgramList = new List<Program>();
        ReportAndReclaimList = new List<ReportAndReclaim>();
        ScholarshipAndGrantList = new List<ScholarshipAndGrant>();


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


                    //Felkontroll för att vara säker på att kolumnerna i Excel finns i Dictionaryt:
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


                                PropertyInfo prop = modelType.GetProperty(colName);

                                if (prop != null)
                                {
                                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                    var propValue = FormatCellValue(propType, cellData, colName, row, col);
                                    prop.SetValue(modelInstance, propValue);
                                }
                            }
                        }
                    }

                    AddObjectsToLists(modelInstances);
                }
            }

            context.ApplicationAndEvaluations.AddRange(ApplicationAndEvaluationList);
            context.Organizations.AddRange(OrganizationList);
            context.Participants.AddRange(ParticipantList);
            context.Payments.AddRange(PaymentList);
            context.PreviousApplications.AddRange(PreviousApplicationList);
            context.Programs.AddRange(ProgramList);
            context.ReportAndReclaims.AddRange(ReportAndReclaimList);
            context.ScholarshipAndGrants.AddRange(ScholarshipAndGrantList);

            context.SaveChanges();
        }
    }


    private void AddObjectsToLists(Dictionary<Type, object> modelInstances)
    {
        foreach (var model in modelInstances)
        {
            var modelType = model.Key;
            var modelInstance = model.Value;

            PropertyInfo listProp = GetType().GetProperty(modelType.Name + "List");

            if (listProp != null)
            {
                IList list = (IList)listProp.GetValue(this);

                if (HasNonDefaultProperties(modelInstance))
                {
                    list.Add(modelInstance);
                }
            }
        }
    }


    private bool HasNonDefaultProperties(object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();

        foreach (var prop in properties)
        {
            var defaultValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
            var value = prop.GetValue(obj);

            if (value == null)
            {
                continue;
            }

            if (!value.Equals(defaultValue))
            {
                return true;
            }
        }

        return false;
    }


    private object FormatCellValue(Type propType, string cellData, string colName, int row, int col)
    {
        //Meddelande som kan användas för felsökning av NULL-värden osv
        //Här ser vi vilken data som skrivs till vilken kolumn i databasen,
        //vad kolumnen heter i Excel-filen och vilken rad och vilket kolumnnummer på den raden

        //Vi skulle kunna logga detta på något vis i den färdiga applikationen, det hade varit snyggt
        Console.WriteLine($"Formatting cell data: {cellData} to type: {propType.Name} in column: {colName} on row {row}, column {col}" );

        switch (Type.GetTypeCode(propType))
        {
            case TypeCode.Int32:
                int intValue;
                return int.TryParse(cellData, NumberStyles.Any, CultureInfo.InvariantCulture, out intValue) ? intValue : null;
            case TypeCode.DateTime:
                if (string.IsNullOrWhiteSpace(cellData))
                    return null;
                DateTime dateValue;
                return DateTime.TryParseExact(cellData, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue) ? dateValue : null;
            case TypeCode.Single:
                float floatValue;
                return float.TryParse(cellData, NumberStyles.Any, CultureInfo.InvariantCulture, out floatValue) ? floatValue : null;
            default:
                return Convert.ChangeType(cellData, propType, CultureInfo.InvariantCulture);
        }
    }


    private void CheckIfNoOfColumnsMatch(Dictionary<string, Type> HeaderProperties, int colCount)
    {
        if (HeaderProperties.Count < colCount)
        {
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




    public void GenerateNewDictionaries()
    {
        DictionarySetup dictionary = new();
    }



}