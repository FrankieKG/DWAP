using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models.POCO
{
  public class Program
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProgramId { get; set; }
    [Required]
    [MaxLength(255)]
    public string ProgramName { get; set; }
    [MaxLength(255)]
    public string EducationLevel { get; set; }
    public string EducationalProgram { get; set; }
    public string Subject { get; set; }
    public string Semester { get; set; }
    public int Weeks { get; set; }
    public string PartnerSchool { get; set; }
    public string PartnerCity { get; set; }
    public string PartnerCountry { get; set; }
    public string PartnerSchool_EducationLevel { get; set; }
    public string GrantArea { get; set; }
    public string From_Country { get; set; }
    public string To_Country { get; set; }
  }
}
