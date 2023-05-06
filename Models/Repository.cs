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
                    CheckIfColumnsMatch(HeaderProperties, colCount);


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

                                //HÄRUNDER SMÄLLER DET
                                //Prop är tydligen alltid null, men fan vet varför asså

                                if (prop != null)
                                {
                                    
                                    //Konverterar celldata för att matcha tabelldatan:
                                    var value = Convert.ChangeType(cellData, prop.PropertyType);
                                    prop.SetValue(modelInstance, value);
                                }
                                else
                                {
                                    Console.WriteLine("The class does not have such an attribute");
                                }
                            }
                        }
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
    }


    private void CheckIfColumnsMatch(Dictionary<string, Type> HeaderProperties, int colCount)
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

        Dictionary<string, Type> headerProperties = new Dictionary<string, Type>();
        foreach (string columnName in columnNames)
        {
            if (columnMappings.TryGetValue(columnName, out var mapping))
            {
                headerProperties[columnName] = mapping.Item2;
            }
            else
            {
                throw new Exception("Column " + columnName + " mismatch");
            }
        }


        return headerProperties;
    }








    public void DictionaryGeneration()
    {

        Dictionary<string, (string, Type)> columnMappings = new Dictionary<string, (string, Type)>
        {
            { "Period", ("Period", typeof(ApplicationAndEvaluation)) },
            { "Perioddatum", ("PeriodDate", typeof(ApplicationAndEvaluation)) },
            { "Ramärendenummer", ("FrameCaseNumber", typeof(ApplicationAndEvaluation)) },
            {"Ansökansstatus", ("ApplicationStatus", typeof(ApplicationAndEvaluation)) },
            { "Dnr", ("Dnr", typeof(ApplicationAndEvaluation)) },
            {"Tema", ("Theme", typeof(ApplicationAndEvaluation)) },
            {"Typ av utbyte", ("Exchange_Type", typeof(ApplicationAndEvaluation)) },
            {"Viktad kvalitetspoäng från budgeteringsvyn", ("Weighted_QualityPoints_BudgetView", typeof(ApplicationAndEvaluation)) },
            {"Medelbetyg totalpoäng ansökan delat i antal bedömare hämtas från från budgeteringsvyn", ("Average_TotalPoints_Application", typeof(ApplicationAndEvaluation)) },
            {"Poängdifferens från ansökningsvyn", ("PointDifference_ApplicationView", typeof(ApplicationAndEvaluation)) },
            {"Kvalitetspoäng rapport", ("QualityPoints_Report", typeof(ApplicationAndEvaluation)) },
            {"Viktad medelpoäng", ("Weighted_AveragePoints", typeof(ApplicationAndEvaluation)) },
            {"Medelbetyg", ("AverageRating", typeof(ApplicationAndEvaluation)) },
            {"Poängdifferens", ("PointDifference", typeof(ApplicationAndEvaluation)) },
            {"Arkiverat datum", ("Archived_Date", typeof(ApplicationAndEvaluation)) },
            { "Organisation", ("OrganizationName", typeof(Organization)) },
            {"Organisationsepost", ("OrganizationEmail", typeof(Organization)) },
            {"Postnummer", ("PostalCode", typeof(Organization)) },
            {"Ort", ("City", typeof(Organization)) },
            {"Kommun", ("Municipality", typeof(Organization)) },
            {"Län", ("County", typeof(Organization)) },
            {"Kontoinnehavare", ("AccountHolder", typeof(Organization)) },
            {"Organisationsnummer", ("OrganizationNumber", typeof(Organization)) },
            {"Plus/bankgiro", ("Plus_Bankgiro", typeof(Organization)) },
            {"Organisations epost", ("OrganizationEmail", typeof(Organization)) },
            { "Rapportstatus", ("Report_Status", typeof(ReportAndReclaim)) },
            { "Rapportstatusdatum", ("ReportStatusDate", typeof(ReportAndReclaim)) },
            {"Datum för när rapportstatus är satt", ("Date_when_ReportStatus_Set", typeof(ReportAndReclaim)) },
            {"Status rapport", ("Status_Report", typeof(ReportAndReclaim)) },
            {"Återkrav inbetalt datum", ("Reclaim_Paid_Date", typeof(ReportAndReclaim)) },
            {"Återkrav summa", ("Reclaim_Amount", typeof(ReportAndReclaim)) },
            {"Återkrav inbetalt summa", ("Reclaim_Paid_Amount", typeof(ReportAndReclaim)) },
            { "Förnamn", ("FirstName", typeof(Participant)) },
            { "Efternamn", ("LastName", typeof(Participant)) },
            { "Födelsedata", ("BirthData", typeof(Participant)) },
            { "Kön", ("Gender", typeof(Participant)) },
            { "Land", ("Country", typeof(Participant)) },
            { "Nivå", ("Level", typeof(Participant)) },
            {"Sökt antal personal/lärare", ("Applied_Staff_Teacher_Number", typeof(Participant)) },
            {"Rapporterat antal personal/lärare", ("Reported_Staff_Teacher_Number", typeof(Participant)) },
            {"Sökt antal elever", ("Applied_Student_Number", typeof(Participant)) },
            {"Rapporterat antal kvinnor (elev)", ("Reported_Women_Student_Number", typeof(Participant)) },
            {"Rapporterat antal män (elev)", ("Reported_Men_Student_Number", typeof(Participant)) },
            {"Rapporterat antal kvinnor (lärare)", ("Reported_Women_Teacher_Number", typeof(Participant)) },
            {"Rapporterat antal män (lärare)", ("Reported_Men_Teacher_Number", typeof(Participant)) },
            {"Rapporterat antal kvinnor (skolledare)", ("Reported_Women_SchoolLeader_Number", typeof(Participant)) },
            {"Rapporterat antal män (skolledare)", ("Reported_Men_SchoolLeader_Number", typeof(Participant)) },
            {"Rapporterat antal kvinnor (associerad personal)", ("Reported_Women_AssociatedStaff_Number", typeof(Participant)) },
            {"Rapporterat antal män (associerad personal)", ("Reported_Men_AssociatedStaff_Number", typeof(Participant)) },
            {"Beviljat antal elever", ("Granted_Student_Number", typeof(Participant)) },
            {"Rapporterat antal elever", ("Reported_Student_Number", typeof(Participant)) },
            {"Godkänt antal elever", ("Approved_Student_Number", typeof(Participant)) },
            {"Sökt antal personal", ("Applied_Staff_Number", typeof(Participant)) },
            {"Beviljat antal personal", ("Granted_Staff_Number", typeof(Participant)) },
            {"Rapporterat antal personal", ("Repported_Staff_Number", typeof(Participant)) },
            {"Godkänt antal personal", ("Approved_Staff_Number", typeof(Participant)) },
            {"Medföljande stödpersonal?", ("Accompanying_Support_Staff", typeof(Participant)) },
            {"Sökt antal deltagare", ("Applied_Participant_Number", typeof(Participant)) },
            {"Beviljat antal deltagare", ("Granted_Participant_Number", typeof(Participant)) },
            {"Rapporterat antal deltagare", ("Reported_Participant_Number", typeof(Participant)) },
            { "Program", ("ProgramName", typeof(Program)) },
            { "Termin", ("Semester", typeof(Program)) },
            { "Ämne", ("Subject", typeof(Program)) },
            { "Veckor", ("Weeks", typeof(Program)) },
            {"Bidragsområde", ("GrantArea", typeof(Program)) },
            {"Från land", ("From_Country", typeof(Program)) },
            {"Till land", ("To_Country", typeof(Program)) },
            {"Partnerskola", ("PartnerSchool", typeof(Program)) },
            {"Partnerort", ("PartnerCity", typeof(Program)) },
            {"Partnerland", ("PartnerCountry", typeof(Program)) },
            {"Utbildningsnivå", ("EducationLevel", typeof(Program)) },
            {"Utbildningsnivå partnerskola", ("PartnerSchool_EducationLevel", typeof(Program)) },
            {"Utbildningsprogram", ("EducationalProgram", typeof(Program)) },
            {"Sökt År/månad", ("Applied_Year_Month", typeof(ScholarshipAndGrant)) },
            {"Rapporterat år/månad", ("Reported_Year_Month", typeof(ScholarshipAndGrant)) },
            {"Sökt antal dagar", ("Applied_Number_Of_Days", typeof(ScholarshipAndGrant)) },
            {"Rapporterat antal dagar", ("Reported_Number_Of_Days", typeof(ScholarshipAndGrant)) },
            {"Projekt", ("Project", typeof(ScholarshipAndGrant)) },
            {"Projektår", ("ProjectYear", typeof(ScholarshipAndGrant)) },
            {"Antal", ("NumberOfGrantedScholarships", typeof(ScholarshipAndGrant)) },
            {"Totalt Sökt belopp", ("Total_Applied_Amount", typeof(Payment)) },
            {"Totalt Beviljat belopp", ("Total_Granted_Amount", typeof(Payment)) },
            {"Totalt Rapporterat belopp", ("Total_Reported_Amount", typeof(Payment)) },
            {"Totalt Godkänt belopp", ("Total_Approved_Amount", typeof(Payment)) },
            {"Utbetalning", ("PaymentAmount", typeof(Payment)) },
            {"Totalt sökt belopp", ("Total_Applied_Amount", typeof(Payment)) },
            {"Totalt beviljat belopp", ("Total_Granted_Amount", typeof(Payment)) },
            {"Totalt rapporterat belopp", ("Total_Reported_Amount", typeof(Payment)) },
            {"Totalt godkänt rapporterat belopp", ("Total_Approved_Amount", typeof(Payment)) },
            {"Sökt belopp extramedel", ("Applied_Amount_ExtraFunds", typeof(Payment)) },
            {"Beviljat belopp extramedel", ("Granted_Amount_ExtraFunds", typeof(Payment)) },
            {"Rapporterat belopp extramedel", ("Reported_Amount_ExtraFunds", typeof(Payment)) },
            {"Godkänt/justerat belopp extramedel", ("Approved_Adjusted_Amount_ExtraFunds", typeof(Payment)) }
        };


        string jsonString = JsonConvert.SerializeObject(columnMappings);
        File.WriteAllText("dictionary.json", jsonString);
    }


}