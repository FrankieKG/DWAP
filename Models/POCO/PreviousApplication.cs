using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
  public class PreviousApplication
  {
    [Key]
    public string Dnr{ get; set; }

    [Required]
    public string PreviousDnr { get; set; }    
  }
}
