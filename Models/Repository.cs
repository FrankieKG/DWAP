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