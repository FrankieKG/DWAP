using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        //Här ska dataseten för klasserna in
        
    }
    
    
}