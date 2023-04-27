namespace WebApplication5.Models;

public interface IRepository
{
    //Signaturer för alla metoder som ska finnas i Repository


    Task ReadFile(IFormFile file);

}