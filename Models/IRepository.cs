namespace WebApplication5.Models;

public interface IRepository
{
    //Signaturer för alla metoder som ska finnas i Repository

    //Här ska queryset för POCO-klasser läggas in (IQueryable<POCO-klass>) så att vi kan få ut listor av alla klasser
    //Dessa definieras i Repositoryt (

    Task ReadFile(IFormFile file);

}