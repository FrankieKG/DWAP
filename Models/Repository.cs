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

    private readonly ApplicationDbContext context;
    
    public Repository(ApplicationDbContext context)
    {
      this.context = context;
    }
    
    
    public IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations => context.ApplicationAndEvaluations;

    //Reads data from an Excel file and converts it to fields in POCO classes
    public async Task ReadFile(IFormFile file)
    {
    }
    
    
}