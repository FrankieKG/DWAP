using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models.POCO
{
  public class Organization
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrganizationId { get; set; }
    [Required]
    [MaxLength(255)]
    public string OrganizationName { get; set; }
    [MaxLength(255)]
    public string OrganizationEmail { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Municipality { get; set; }
    public string County { get; set; }
    public string AccountHolder { get; set; }
    public string OrganizationNumber { get; set; }
    public string Plus_Bankgiro { get; set; }
  }
}
