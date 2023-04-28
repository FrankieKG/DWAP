using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class PreviousApplication
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ApplicationId { get; set; }
    [MaxLength(255)]
    public string PreviousDnr { get; set; }
    [ForeignKey("ApplicationId")]
    public virtual ApplicationAndEvaluation Application { get; set; }
    [ForeignKey("PreviousDnr")]
    public virtual ApplicationAndEvaluation PreviousApplicationDnr { get; set; }
  }
}
