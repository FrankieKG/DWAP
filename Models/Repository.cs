namespace WebApplication5.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

public class Repository : IRepository
{
    
    //Read data from an Excel file and converts it to fields in POCO classes
    public async Task ReadFile(IFormFile file)
    {
        
        //TODO: Skriv kod här
        
    }
    
}