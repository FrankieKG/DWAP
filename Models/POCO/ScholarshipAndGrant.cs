namespace WebApplication5.Models.POCO
{
  public class ScholarshipAndGrant
  {
    public int ScholarshipId { get; set; }
    public string PreviousApplication_Dnr { get; set; }
    public string Project { get; set; }
    public int ProjectYear { get; set; }
    public DateTime Applied_Year_Month { get; set; }
    public DateTime Reported_Year_Month { get; set; }
    public int Applied_Number_Of_Days { get; set; }
    public int Reported_Number_Of_Days { get; set; }
    public int NumberOfAppliedScholarships { get; set; }
    public int NumberOfGrantedScholarships { get; set; }
    public int ApplicationId { get; set; }
    public virtual ApplicationAndEvaluation Application { get; set; }
  }
}
