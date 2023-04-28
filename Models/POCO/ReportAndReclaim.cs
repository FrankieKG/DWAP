using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class ReportAndReclaim
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReportId { get; set; }
    [MaxLength(255)]
    public string ReportStatus { get; set; }
    public DateTime ReportStatusDate { get; set; }
    public DateTime Date_when_ReportStatus_Set { get; set; }
    public string Status_Report { get; set; }
    public DateTime Reclaim_Paid_Date { get; set; }
    public float Reclaim_Amount { get; set; }
    public float Reclaim_Paid_Amount { get; set; }
    public DateTime Reclaim_Created_Date { get; set; }
    public int NumberOfReportedScholarships { get; set; }
    public int NumberOfReportedCompletedScholarships { get; set; }
    public int NumberOfReportedAbortedScholarships { get; set; }
    public int NumberOfReportedNotAwardedScholarships { get; set; }
    public int ApplicationId { get; set; }
    [ForeignKey(nameof(ApplicationId))]
    public virtual ApplicationAndEvaluation Application { get; set; }
  }

}
