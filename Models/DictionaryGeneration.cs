using Newtonsoft.Json;

namespace WebApplication5.Models
{
    public class DictionaryGeneration
    {

        public DictionaryGeneration()
        {
            GenerateDictionaryFile();
        }

        public void GenerateDictionaryFile()
        {
            Dictionary<string, (string, Type)> columnMappings = new Dictionary<string, (string, Type)>
            {
                {"Period", ("Period", typeof(ApplicationAndEvaluation)) },
                {"Perioddatum", ("PeriodDate", typeof(ApplicationAndEvaluation)) },
                {"Ramärendenummer", ("FrameCaseNumber", typeof(ApplicationAndEvaluation)) },
                {"Ansökansstatus", ("ApplicationStatus", typeof(ApplicationAndEvaluation)) },
                {"Dnr", ("Dnr", typeof(ApplicationAndEvaluation)) },
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
                {"Organisation", ("OrganizationName", typeof(Organization)) },
                {"Organisationsepost", ("OrganizationEmail", typeof(Organization)) },
                {"Postnummer", ("PostalCode", typeof(Organization)) },
                {"Ort", ("City", typeof(Organization)) },
                {"Kommun", ("Municipality", typeof(Organization)) },
                {"Län", ("County", typeof(Organization)) },
                {"Kontoinnehavare", ("AccountHolder", typeof(Organization)) },
                {"Organisationsnummer", ("OrganizationNumber", typeof(Organization)) },
                {"Plus/bankgiro", ("Plus_Bankgiro", typeof(Organization)) },
                {"Organisations epost", ("OrganizationEmail", typeof(Organization)) },
                {"Rapportstatus", ("Report_Status", typeof(ReportAndReclaim)) },
                {"Rapportstatusdatum", ("ReportStatusDate", typeof(ReportAndReclaim)) },
                {"Datum för när rapportstatus är satt", ("Date_when_ReportStatus_Set", typeof(ReportAndReclaim)) },
                {"Status rapport", ("Status_Report", typeof(ReportAndReclaim)) },
                {"Återkrav inbetalt datum", ("Reclaim_Paid_Date", typeof(ReportAndReclaim)) },
                {"Återkrav summa", ("Reclaim_Amount", typeof(ReportAndReclaim)) },
                {"Återkrav skapat datum", ("Reclaim_Created_Date", typeof(ReportAndReclaim)) },
                {"Antal rapporterade stipendier", ("NumberOfReportedScholarships", typeof(ReportAndReclaim)) },
                {"Antal rapporterade genomförda stipendier", ("NumberOfReportedCompletedScholarships", typeof(ReportAndReclaim)) },
                {"Antal rapporterade avbrutna stipendier", ("NumberOfReportedAbortedScholarships", typeof(ReportAndReclaim)) },
                {"Antal rapporterade ej utdelade stipendier", ("NumberOfReportedNotAwardedScholarships", typeof(ReportAndReclaim)) },
                {"Återkrav inbetalt summa", ("Reclaim_Paid_Amount", typeof(ReportAndReclaim)) },
                {"Förnamn", ("FirstName", typeof(Participant)) },
                {"Efternamn", ("LastName", typeof(Participant)) },
                {"Födelsedata", ("BirthData", typeof(Participant)) },
                {"Kön", ("Gender", typeof(Participant)) },
                {"Land", ("Country", typeof(Participant)) },
                {"Nivå", ("Level", typeof(Participant)) },
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
                {"Program", ("ProgramName", typeof(Program)) },
                {"Termin", ("Semester", typeof(Program)) },
                {"Ämne", ("Subject", typeof(Program)) },
                {"Veckor", ("Weeks", typeof(Program)) },
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
}
