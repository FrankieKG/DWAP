namespace WebApplication5.Models
{
  using OfficeOpenXml;
  using System.Collections.Generic;
  using System.IO;
  public class Excel_importer
  {
    public List<ApplicationAndEvaluation> ApplicationAndEvaluation { get; set; }
    public List<Organization> Organization { get; set; }
    public List<Participant> Participant { get; set; }
    public List<Payment> Payment { get; set; }
    public List<PreviousApplication> PreviousApplication { get; set; }
    public List<Program> Program { get; set; }
    public List<ReportAndReclaim> ReportAndReclaim { get; set; }
    public List<ScholarshipAndGrant> ScholarshipAndGrant { get; set; }

    public Excel_importer(string filePath, Dictionary<string, string> columnMappings)
    {
     
      ApplicationAndEvaluation = new List<ApplicationAndEvaluation>();
      Organization = new List<Organization>(); 
      Participant = new List<Participant>();
      Payment = new List<Payment>();
      PreviousApplication = new List<PreviousApplication>();
      Program = new List<Program>();
      ReportAndReclaim = new List<ReportAndReclaim>();
      ScholarshipAndGrant = new List<ScholarshipAndGrant>();

      FileInfo file = new FileInfo(filePath);


      using (ExcelPackage package = new ExcelPackage(file))
      {
        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        int rowCount = worksheet.Dimension.Rows;
        int colCount = worksheet.Dimension.Columns;

        var headerCells = worksheet.Cells[1, 1, 1, colCount];
        var headers = headerCells.ToDictionary(x => x.Start.Column, x => x.Text);

        int GetColumnIndexByName(string columnName)
        {
          return headers.FirstOrDefault(x => x.Value.Equals(columnName, StringComparison.OrdinalIgnoreCase)).Key;
        }

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

          foreach (var propertyMapping in columnMappings)
          {
            var propertyName = propertyMapping.Key;
            var columnName = propertyMapping.Value;

            var propertyInfo = typeof(ApplicationAndEvaluation).GetProperty(propertyName);
            var cellValue = worksheet.Cells[row, GetColumnIndexByName(columnName)].Text;

            if (propertyInfo != null)
            {
              var convertedValue = Convert.ChangeType(cellValue, propertyInfo.PropertyType);
              propertyInfo.SetValue(applicationAndEvaluations, convertedValue);
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
 
   
}
}
