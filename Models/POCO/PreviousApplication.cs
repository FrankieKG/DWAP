using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class PreviousApplication
  {
    [Key, Column(Order = 0)]
    public int ApplicationId { get; set; }
    [Key, Column(Order = 1)]
    [MaxLength(255)]
    public string PreviousDnr { get; set; }
    [ForeignKey("ApplicationId")]
    public virtual ApplicationAndEvaluation Application { get; set; }
    [ForeignKey("PreviousDnr")]
    public virtual ApplicationAndEvaluation PreviousApplicationDnr { get; set; }
  }
}
