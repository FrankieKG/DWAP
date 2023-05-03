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

    //Reads data from an Excel file and converts it to fields in POCO classes
    public async Task ReadFile(IFormFile file)
    {
    ar columnMappings = new Dictionary<string, string>
{
    { "Period", "Period" },
    { "Perioddatum", "PeriodDate" },
    { "Ramärendenr", "FrameCaseNumber" },
    { "Program", "Program" },
    { "Dnr", "Dnr" },
    { "Organisation", "Organization" },
    { "Rapportstatus", "ReportStatus" },
    { "Rapportstatusdatum", "ReportStatusDate" },
      {"Antal", "" },
    { "Förnamn", "FirstName" },
    { "Efternamn", "LastName" },
    { "Födelsedata", "BirthData" },
    { "Kön", "Gender" },
    { "Land", "Country" },
    { "Nivå", "Level" },
    { "Termin", "Semester" },
    { "Ämne", "Subject" },
    { "Veckor", "Weeks" },
      {"Ramärendenummer", "" },
      {"Ansökanstatus", "" },
      {"Organisationsepost", "" },
      {"Postnummer", "" },
      {"Ort", "" },
      {"Kommun", "" },
      {"Län", "" },
      {"Kontoinnehavare", "" },
      {"Organisationsnummer", "" },
      {"Plus/bankgiro", "" },
      {"Utbetalning", "" },
      {"Projekt", "" },
      {"Projektår", "" },
      {"Partnerskola", "" },
      {"Partnerort", "" },
      {"Partnerland", "" },
      {"Utbildningsnivå partnerskola", "" },
      {"Bidragsområde", "" },
      {"Från land", "" },
      {"Till land", "" },
      {"Sökt År/månad", "" },
      {"Rapporterat år/månad", "" },
      {"Sökt antal dagar", "" },
      {"Rapporterat antal dagar", "" },
      {"Tema", "" },
      {"Typ av utbyte", "" },
      {"Viktad kvalitetspoäng från budgeteringsvyn", "" },
      {"Medelbetyg totalpoäng ansökan delat i antal bedömare hämtas från från budgeteringsvyn", "" },
      {"Poängdifferens från ansökningsvyn", "" },
      {"Totalt Sökt belopp", "" },
      {"Totalt Beviljat belopp", "" },
      {"Totalt Rapporterat belopp", "" },
      {"Totalt Godkänt belopp", "" },
      {"Sökt antal deltagare", "" },
      {"Beviljat antal deltagare", "" },
      {"Rapporterat antal deltagare", "" },
      {"Kvalitetspoäng rapport", "" },
      {"Datum för när rapportstatus är satt", "" },
      {"Status rapport", "" },
      {"Återkrav inbetalt datum", "" },
      {"Återkrav summa", "" },
      {"Återkrav inbetalt summa", "" },
      {"Sökt antal personal/lärare", "" },
      {"Rapporterat antal personal/lärare", "" },
      {"Sökt antal elever", "" },
      {"Rapporterat antal elever", "" },
      {"Rapporterat antal kvinnor (elev)", "" },
      {"Rapporterat antal män (elev)", "" },
      {"Rapporterat antal kvinnor (lärare)", "" },
      {"Rapporterat antal män (lärare)", "" },
      {"Rapporterat antal kvinnor (skolledare)", "" },
      {"Rapporterat antal män (skolledare)", "" },
      {"Rapporterat antal kvinnor (associerad personal)", "" },
      {"Rapporterat antal män (associerad personal)", "" },
      {"Organisations epost", "" },
      {"Viktad medelpoäng", "" },
      {"Medelbetyg", "" },
      {"Poängdifferens", "" },
      {"Totalt sökt belopp", "" },
      {"Totalt beviljat belopp", "" },
      {"Totalt rapporterat belopp", "" },
      {"Totalt godkänt rapporterat belopp", "" },
      {"Sökt belopp extramedel", "" },
      {"Beviljat belopp extramedel", "" },
      {"Rapporterat belopp extramedel", "" },
      {"Godkänt/justerat belopp extramedel", "" },
      {"Sökt antal elever", "" },
      {"Beviljat antal elever", "" },
      {"Rapporterat antal elever", "" },
      {"Godkänt antal elever", "" },
      {"Sökt antal personal", "" },
      {"Beviljat antal personal", "" },
      {"Rapporterat antal personal", "" },
      {"Godkänt antal personal", "" },
      {"Medföljande stödpersonal?", "" },
    { "Arkiverat datum", "Archived_Date" },
    { "Utbildningsprogram", "EducationalProgram"}
};
    //Open the Excel file using ExcelPackage
    using (var excelPackage = new ExcelPackage(new MemoryStream(formFile.OpenReadStream())))
        {
            //Select the worksheet you want to read data from using Worksheets collection
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
    
            //Read the data from the worksheet by looping through the rows and columns of the selected worksheet
            for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
            {
                ApplicationAndEvaluation newAppli = new ApplicationAndEvaluation(); //Create new instance of POCO class
                newAppli.Field1 = worksheet.Cells[row, 1].Value.ToString(); //Map data to POCO properties
                newAppli.Field2 = worksheet.Cells[row, 2].Value.ToString();
                newAppli.Field3 = worksheet.Cells[row, 3].Value.ToString();
        
                //Write the data to your SQL database
                // ...
            }
        }
        
    }
    
    
    
}