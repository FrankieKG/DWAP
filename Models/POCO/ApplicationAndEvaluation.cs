namespace WebApplication5.Models.POCO
{
  public class ApplicationAndEvaluation
  {
    public int ApplicationId { get; set; }
    public string FrameCaseNumber { get; set; }
    public string Dnr { get; set; }
    public string ApplicationStatus { get; set; }
    public string Period { get; set; }
    public DateTime PeriodDate { get; set; }
    public DateTime Archived_Date { get; set; }
    public string Accompanying_SupportStaff { get; set; }
    public string Theme { get; set; }
    public string Exchange_Type { get; set; }
    public float Weighted_QualityPoints_BudgetView { get; set; }
    public float Average_TotalPoints_Application { get; set; }
    public float PointDifference_ApplicationView { get; set; }
    public float Weighted_AveragePoints { get; set; }
    public float AverageRating { get; set; }
    public float PointDifference { get; set; }
    public float QualityPoints_Report { get; set; }
    public int OrganizationId { get; set; }
    public int ProgramId { get; set; }

    public virtual Organization Organization { get; set; }
    public virtual Program Program { get; set; }
  }
}
