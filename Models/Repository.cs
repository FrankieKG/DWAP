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
    var columnMappings = new Dictionary<string, string>
{
    { "Period", "Period" },
    { "Perioddatum", "PeriodDate" },
    { "Ramärendenr", "FrameCaseNumber" },
    { "Program", "Program" },
    { "Dnr", "Dnr" },
    { "Organisation", "Organization" },
    { "Rapportstatus", "Report_Status" },
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
    {"Ramärendenummer", "FrameCaseNumber" },
    {"Ansökanstatus", "ApplicationStatus" },
    {"Organisationsepost", "OrganizationEmail" },
    {"Postnummer", "PostalCode" }, 
    {"Ort", "City" }, 
    {"Kommun", "Municipality" }, 
    {"Län", "County" }, 
    {"Kontoinnehavare", "AccountHolder" }, 
    {"Organisationsnummer", "OrganizationNumber" }, 
    {"Plus/bankgiro", "Plus_Bankgiro" }, 
    {"Utbetalning", "PaymentAmount" }, 
    {"Projekt", "Project" }, 
    {"Projektår", "ProjectYear" }, 
    {"Partnerskola", "PartnerSchool" }, 
    {"Partnerort", "PartnerCity" }, 
    {"Partnerland", "PartnerCountry" }, 
    {"Utbildningsnivå partnerskola", "PartnerSchool_EducationLevel" }, 
    {"Bidragsområde", "GrantArea" }, 
    {"Från land", "From_Country" }, 
    {"Till land", "To_Country" }, 
    {"Sökt År/månad", "Applied_Year_Month" }, 
    {"Rapporterat år/månad", "Reported_Year_Month" }, 
    {"Sökt antal dagar", "Applied_Number_Of_Days" }, 
    {"Rapporterat antal dagar", "Reported_Number_Of_Days" }, 
    {"Tema", "Theme" }, 
    {"Typ av utbyte", "Exchange_Type" }, 
    {"Viktad kvalitetspoäng från budgeteringsvyn", "Weighted_QualityPoints_BudgetView" }, 
    {"Medelbetyg totalpoäng ansökan delat i antal bedömare hämtas från från budgeteringsvyn", "Average_TotalPoints_Application" }, 
    {"Poängdifferens från ansökningsvyn", "PointDifference_ApplicationView" }, 
    {"Totalt Sökt belopp", "Total_Applied_Amount" }, 
    {"Totalt Beviljat belopp", "Total_Granted_Amount" }, 
    {"Totalt Rapporterat belopp", "Total_Reported_Amount" }, 
    {"Totalt Godkänt belopp", "Total_Approved_Amount" }, 
    {"Sökt antal deltagare", "Applied_Participant_Number" }, 
    {"Beviljat antal deltagare", "Granted_Participant_Number" }, 
    {"Rapporterat antal deltagare", "Reported_Participant_Number" }, 
    {"Kvalitetspoäng rapport", "QualityPoints_Report" }, 
    {"Datum för när rapportstatus är satt", "Date_when_ReportStatus_Set" }, 
    {"Status rapport", "Report_Status" }, 
    {"Återkrav inbetalt datum", "Reclaim_Paid_Date" }, 
    {"Återkrav summa", "Reclaim_Amount" }, 
    {"Återkrav inbetalt summa", "Reclaim_Paid_Amount" }, 
    {"Sökt antal personal/lärare", "Applied_Staff_Teacher_Number" }, 
    {"Rapporterat antal personal/lärare", "Reported_Staff_Teacher_Number" }, 
    {"Sökt antal elever", "Applied_Student_Number" }, 
    {"Rapporterat antal kvinnor (elev)", "Reported_Women_Student_Number" }, 
    {"Rapporterat antal män (elev)", "Reported_Men_Student_Number" }, 
    {"Rapporterat antal kvinnor (lärare)", "Reported_Women_Teacher_Number" }, 
    {"Rapporterat antal män (lärare)", "Reported_Men_Teacher_Number" }, 
    {"Rapporterat antal kvinnor (skolledare)", "Reported_Women_SchoolLeader_Number" }, 
    {"Rapporterat antal män (skolledare)", "Reported_Men_SchoolLeader_Number" }, 
    {"Rapporterat antal kvinnor (associerad personal)", "Reported_Women_AssociatedStaff_Number" }, 
    {"Rapporterat antal män (associerad personal)", "Reported_Men_AssociatedStaff_Number" }, 
    {"Organisations epost", "OrganizationEmail" }, 
    {"Viktad medelpoäng", "Weighted_AveragePoints" }, 
    {"Medelbetyg", "AverageRating" }, 
    {"Poängdifferens", "PointDifference" }, 
    {"Totalt sökt belopp", "Total_Applied_Amount" }, 
    {"Totalt beviljat belopp", "Total_Granted_Amount" }, 
    {"Totalt rapporterat belopp", "Total_Reported_Amount" }, 
    {"Totalt godkänt rapporterat belopp", "Total_Approved_Amount" }, 
    {"Sökt belopp extramedel", "Applied_Amount_ExtraFunds" }, 
    {"Beviljat belopp extramedel", "Granted_Amount_ExtraFunds" }, 
    {"Rapporterat belopp extramedel", "Reported_Amount_ExtraFunds" }, 
    {"Godkänt/justerat belopp extramedel", "Approved_Adjusted_Amount_ExtraFunds" }, 
    {"Beviljat antal elever", "Granted_Student_Number" }, 
    {"Rapporterat antal elever", "Reported_Student_Number" }, 
    {"Godkänt antal elever", "Approved_Student_Number" }, 
    {"Sökt antal personal", "Applied_Staff_Number" }, 
    {"Beviljat antal personal", "Granted_Staff_Number" }, 
    {"Rapporterat antal personal", "Repported_Staff_Number" }, 
    {"Godkänt antal personal", "Approved_Staff_Number" }, 
    {"Medföljande stödpersonal?", "Accompanying_Support_Staff" }, 
    {"Arkiverat datum", "Archived_Date" }, 
    {"Utbildningsprogram", "EducationalProgram"}
};

    ExcelImporter import = new ExcelImporter(file, columnMappings);
    
    context.ApplicationAndEvaluations.AddRange(import.ApplicationAndEvaluation);
    context.Organizations.AddRange(import.Organization);
    context.Participants.AddRange(import.Participant);
    context.Payments.AddRange(import.Payment);
    context.PreviousApplications.AddRange(import.PreviousApplication);
    context.Programs.AddRange(import.Program);
    context.ReportAndReclaims.AddRange(import.ReportAndReclaim);
    context.ScholarshipAndGrants.AddRange(import.ScholarshipAndGrant);

    context.SaveChanges();
    }
}