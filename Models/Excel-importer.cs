namespace WebApplication5.Models
{
  using OfficeOpenXml;
  using System.Collections.Generic;
  using System.IO;
  public class Excel_importer
  {
  public List<Organization> ReadOrganizationsFromExcel(string filePath)
  {
    var organizations = new List<Organization>();
    FileInfo file = new FileInfo(filePath);

    using (ExcelPackage package = new ExcelPackage(file))
    {
      // Assuming the data is in the first worksheet
      ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
      int rowCount = worksheet.Dimension.Rows;

      for (int row = 2; row <= rowCount; row++)
      {
        var organization = new Organization
        {
          OrganizationId = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
          OrganizationName = worksheet.Cells[row, 2].Value.ToString(),
          // ... Add other properties here
        };

        organizations.Add(organization);
      }
    }

    return organizations;
  }


}
}
