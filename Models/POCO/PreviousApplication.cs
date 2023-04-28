namespace WebApplication5.Models.POCO
{
  public class PreviousApplication
  {
    public int ApplicationId { get; set; }
    public string PreviousDnr { get; set; }

    public virtual ApplicationAndEvaluation Application { get; set; }
    public virtual ApplicationAndEvaluation PreviousApplication { get; set; }
  }
}
