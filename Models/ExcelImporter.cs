namespace WebApplication5.Models
{
  using OfficeOpenXml;
  using System.Collections.Generic;
  using System.IO;
  public class ExcelImporter
  {
    public List<ApplicationAndEvaluation> ApplicationAndEvaluation { get; set; }
    public List<Organization> Organization { get; set; }
    public List<Participant> Participant { get; set; }
    public List<Payment> Payment { get; set; }
    public List<PreviousApplication> PreviousApplication { get; set; }
    public List<Program> Program { get; set; }
    public List<ReportAndReclaim> ReportAndReclaim { get; set; }
    public List<ScholarshipAndGrant> ScholarshipAndGrant { get; set; }

    public ExcelImporter(IFormFile file, Dictionary<string, string> columnMappings)
    {
            _ = StartExcelReading(file, columnMappings);
    }
    

    async Task StartExcelReading(IFormFile file, Dictionary<string, string> columnMappings)
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

                var headerCells = worksheet.Cells[1, 1, 1, colCount]; // Första två ettorna är start rad och column,
                                                                      // 1 och colCount är första raden och sista kolumnet

                var headers = headerCells.ToDictionary(x => x.Start.Column, x => x.Text);

                int GetColumnIndexByName(ExcelWorksheet worksheet, string columnName)
                {
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        if (worksheet.Cells[1, col].Text.Trim().ToLower() == columnName.Trim().ToLower())
                        {
                            return col;
                        }
                    }

                    // Return a default value if the column name is not found
                    return -1;
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
    }
    
  }
}
