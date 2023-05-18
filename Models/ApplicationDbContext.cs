using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationAndEvaluation> ApplicationAndEvaluations { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PreviousApplication> PreviousApplications { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ReportAndReclaim> ReportAndReclaims { get; set; }
    public DbSet<ScholarshipAndGrant> ScholarshipAndGrants { get; set; }
    public DbSet<ScholarshipAndGrant> AtlasPartnerskapData { get; set; }
}