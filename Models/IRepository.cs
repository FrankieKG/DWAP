using WebApplication5.Model;
using WebApplication5.Models;

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

    public IQueryable<AtlasPartnerskapData> GetAtlasPartnerskapDnr(string dnr);
    public IQueryable<AtlasPraktikData> GetAtlasPraktikDnr(string dnr);
    public IQueryable<MobilitetsstatistikMFSStipendierData> GetMobilitetsstatistikMFSStipendierDnr(string dnr);
    public IQueryable<MFSStipendierData> GetMFSStipendierDnr(string dnr);
}