namespace WebApplication5.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

public class Repository : IRepository
{
    
    //Read data from an Excel file and converts it to fields in POCO classes
    public async Task ReadFile(IFormFile file)
    {

    //TODO: Skriv kod här

    var fileInfo = new FileInfo("your_excel_file.xlsx");
    using (var package = new ExcelPackage(fileInfo))
    {
      var worksheet = package.Workbook.Worksheets[0]; // Adjust the index according to the desired sheet
      int totalRows = worksheet.Dimension.Rows;

      using (var dbContext = new MyDbContext())
      {
        for (int row = 2; row <= totalRows; row++) // Assuming the first row contains column names
        {
          var organizationNameCell = worksheet.Cells[row, GetColumnIndexByName(worksheet, "Organization")];
          string organizationName = organizationNameCell.Value.ToString();

          // Read other cells by column name
          // ...

          // Create instances of POCO classes based on the cell values
          var organization = new Organization
          {
            OrganizationName = organizationName,
            // Set other properties...
          };

          // Add the instances to the DbContext
          dbContext.Organizations.Add(organization);

          // ... Create instances of other POCO classes and add them to the DbContext ...

          // Save the changes to the database
          dbContext.SaveChanges();
        }
      }
    }

    private static int GetColumnIndexByName(ExcelWorksheet worksheet, string columnName)
    {
      int columnIndex = worksheet.Cells["1:1"].FirstOrDefault(cell => cell.Value.ToString() == columnName)?.Start.Column ?? -1;

      if (columnIndex == -1)
      {
        throw new ArgumentException($"Column '{columnName}' not found in the worksheet.");
      }

      return columnIndex;
    }
  

}
    
}