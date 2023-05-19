using WebApplication5.Models.POCO.Utilities;

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
    public IQueryable<AtlasPartnerskapData> GetPeriodAtlasPartnerskap(string fromPeriod, string toPeriod);

    public IQueryable<AtlasPraktikData> GetAtlasPraktikDnr(string dnr);
    public IQueryable<AtlasPraktikData> GetPeriodAtlasPraktik(string fromPeriod, string toPeriod);

    public IQueryable<MobilitetsstatistikMFSStipendierData> GetMobilitetsstatistikMFSStipendierDnr(string dnr);
    public IQueryable<MobilitetsstatistikMFSStipendierData> GetPeriodMobilitetsstatistikMFSStipendier(string fromPeriod, string toPeriod);


    public IQueryable<MFSStipendierData> GetMFSStipendierDnr(string dnr);
}