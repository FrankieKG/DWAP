using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication5.Models.POCO;

namespace WebApplication5.Models
{
  public class PreviousApplication
  {
    [Key, Column(Order = 0)]
    [ForeignKey("ApplicationId")]
    public int ApplicationId { get; set; }
    public virtual ApplicationAndEvaluation ApplicationAndEvaluation { get; set; }
    
    [Key, Column(Order = 1)]
    [MaxLength(255)]
    [ForeignKey("PreviousDnr")]
    public string PreviousDnr { get; set; }
    public virtual ApplicationAndEvaluation PreviousApplicationAndEvaluation { get; set; }

  }
}
