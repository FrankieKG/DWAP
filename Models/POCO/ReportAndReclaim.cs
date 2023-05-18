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
    public DateTime? ReportStatusDate { get; set; }
    public DateTime? Date_when_ReportStatus_Set { get; set; }
    public string Report_Status { get; set; } 
    public DateTime? Reclaim_Paid_Date { get; set; }
    public int? Reclaim_Amount { get; set; }
    public int? Reclaim_Paid_Amount { get; set; }
    public DateTime? Reclaim_Created_Date { get; set; }
    public int? NumberOfReportedScholarships { get; set; }
    public int? NumberOfReportedCompletedScholarships { get; set; }
    public int? NumberOfReportedAbortedScholarships { get; set; }
    public int? NumberOfReportedNotAwardedScholarships { get; set; }

    [Required]
    public string Dnr { get; set; }
  }

}
