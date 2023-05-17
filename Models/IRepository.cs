using WebApplication5.Models.Utilities;

namespace WebApplication5.Models;

public interface IRepository
{
    IQueryable<ApplicationAndEvaluation> ApplicationAndEvaluations { get; }
    IQueryable<Organization> Organizations { get; }
    IQueryable<Participant> Participants { get; }
    IQueryable<Payment> Payments { get; }
    IQueryable<PreviousApplication> PreviousApplications { get; }
    IQueryable<Program> Programs { get; }
    IQueryable<ReportAndReclaim> ReportAndReclaims { get; }
    IQueryable<ScholarshipAndGrant> ScholarshipAndGrants { get; }

    Task ReadFile(IFormFile file);
    void GenerateNewDictionaries();

    public List<AtlasPartnerskapData> GetAtlasPartnerskapDnr(string dnr);
}