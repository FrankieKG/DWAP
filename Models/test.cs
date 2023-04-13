//using System.IO;
//using OfficeOpenXml;

//namespace WebApplication5.Models
//{

//  public class excelRipper
//  {
//    // ...

//    public void ImportDataFromExcel(string filePath)
//    {
//      FileInfo file = new FileInfo(filePath);

//      using (ExcelPackage package = new ExcelPackage(file))
//      {
//        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Första arbetsbladet i Excel-filen
//        int rowCount = worksheet.Dimension.Rows;
//        int colCount = worksheet.Dimension.Columns;

//        for (int row = 2; row <= rowCount; row++) // Börja på rad 2 för att hoppa över kolumnrubrikerna
//        {
//          // Läs data från Excel-filen
//          string column1Value = worksheet.Cells[row, 1].Value.ToString();
//          string column2Value = worksheet.Cells[row, 2].Value.ToString();
//          // ... Läs fler kolumner om det behövs

//          // Skapa och spara en ny post i databasen
//          MyTable newRecord = new MyTable
//          {
//            Column1 = column1Value,
//            Column2 = column2Value,
//            // ... Tilldela fler kolumner om det behövs
//          };
//          dbContext.MyTable.Add(newRecord);
//        }

//        dbContext.SaveChanges(); // Spara ändringarna i databasen
//      }
//    }

//  }
//}