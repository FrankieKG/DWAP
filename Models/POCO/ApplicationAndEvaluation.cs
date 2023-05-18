using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class ApplicationAndEvaluation
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplicationId { get; set; }

    [MaxLength(255)]
    public string FrameCaseNumber { get; set; }
    [MaxLength(255)]
    public string ApplicationStatus { get; set; }
    [MaxLength(255)]
    public string Period { get; set; }
    public string PeriodDate { get; set; }
    public DateTime? Archived_Date { get; set; }
    public string Accompanying_SupportStaff { get; set; }
    public string Theme { get; set; }
    public string Exchange_Type { get; set; }
    public float? Weighted_QualityPoints_BudgetView { get; set; }
    public float? Average_TotalPoints_Application { get; set; }
    public float? PointDifference_ApplicationView { get; set; }
    public float? Weighted_AveragePoints { get; set; }
    public float? AverageRating { get; set; }
    public float? PointDifference { get; set; }
    public int? QualityPoints_Report { get; set; }
    public string PreviousApplications { get; set; }

    [Required]
    public string Dnr { get; set; }
    }
}