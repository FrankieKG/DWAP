namespace WebApplication5.Models;

public interface IRepository
{
    IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations { get; }
    
    
    Task ReadFile(IFormFile file);

}