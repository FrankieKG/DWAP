using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
  public class PreviousApplication
  {
    [Key]
    public int ApplicationId { get; set; }
    public int PreviousDnr { get; set; }
    
    [Required]
    [ForeignKey("ApplicationId")]
    public virtual ApplicationAndEvaluation Application { get; set; }
    
    [Required]
    [ForeignKey("PreviousDnr")]
    public virtual ApplicationAndEvaluation PrevApplication { get; set; }
  }
}
