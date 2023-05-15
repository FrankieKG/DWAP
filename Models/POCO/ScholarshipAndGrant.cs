using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class ScholarshipAndGrant
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ScholarshipId { get; set; }

    [MaxLength(255)]
    public string PreviousApplication_Dnr { get; set; }
    [MaxLength(255)]
    public string Project { get; set; }
    public int? ProjectYear { get; set; }
    public string Applied_Year_Month { get; set; }
    public string Reported_Year_Month { get; set; }
    public int? Applied_Number_Of_Days { get; set; }
    public int? Reported_Number_Of_Days { get; set; }
    public int? NumberOfAppliedScholarships { get; set; }
    public int? NumberOfGrantedScholarships { get; set; }

    [Required]
    public string Dnr { get; set; }
  }
}
