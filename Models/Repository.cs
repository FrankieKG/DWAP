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
        Dictionary<string, string> columnMappings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

        await ExcelImporter(file, columnMappings);
    }


    private async Task ExcelImporter(IFormFile file, Dictionary<string, string> columnMappings)
    {
        List<Type> modelTypes = new List<Type>
        {
        typeof(ApplicationAndEvaluation),
        typeof(Organization),
        typeof(Participant),
        typeof(Payment),
        typeof(PreviousApplication),
        typeof(Program),
        typeof(ReportAndReclaim),
        typeof(ScholarshipAndGrant)
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

                // Första två ettorna är startrad och -kolumn;
                // tredje 1:an och colCount är första raden respektive sista kolumnen
                var headerCells = worksheet.Cells[1, 1, 1, colCount]; 
            

                var headers = headerCells.ToDictionary(x => x.Start.Column, x => x.Text);
                
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

                    foreach (var modelType in modelTypes)
                    {

                        foreach (var propertyMapping in columnMappings)
                        {
                            var propertyName = propertyMapping.Key;
                            var columnName = propertyMapping.Value;

                            var propertyInfo = modelType.GetProperty(propertyName);
                            var cellValue = worksheet.Cells[row, GetColumnIndexByName(worksheet, columnName)].Text;

                            if (propertyInfo != null)
                            {
                                var convertedValue = Convert.ChangeType(cellValue, propertyInfo.PropertyType);

                                if (modelType == typeof(ApplicationAndEvaluation))
                                {
                                    propertyInfo.SetValue(applicationAndEvaluations, convertedValue);
                                }
                                else if (modelType == typeof(Organization))
                                {
                                    propertyInfo.SetValue(organizations, convertedValue);
                                }
                                else if (modelType == typeof(Participant))
                                {
                                    propertyInfo.SetValue(participants, convertedValue);
                                }
                                else if (modelType == typeof(Payment))
                                {
                                    propertyInfo.SetValue(payments, convertedValue);
                                }
                                else if (modelType == typeof(PreviousApplication))
                                {
                                    propertyInfo.SetValue(previousApplications, convertedValue);
                                }
                                else if (modelType == typeof(Program))
                                {
                                    propertyInfo.SetValue(programs, convertedValue);
                                }
                                else if (modelType == typeof(ReportAndReclaim))
                                {
                                    propertyInfo.SetValue(reportAndReclaims, convertedValue);
                                }
                                else if (modelType == typeof(ScholarshipAndGrant))
                                {
                                    propertyInfo.SetValue(scholarshipandgrants, convertedValue);
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
    
    /**
     * Hjälpmetod för att plocka ut kolumnnamn:
     */
    int GetColumnIndexByName(ExcelWorksheet worksheet, string columnName)
    {
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            if (worksheet.Cells[1, col].Text.Trim().ToLower() == columnName.Trim().ToLower())
            {
                return col;
            }
        }
        
        // Returnerar negativt om en kolumn inte kan hittas:
        return -1;
    }

    
}