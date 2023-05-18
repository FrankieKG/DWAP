using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class Payment
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }
    
    public int? PaymentAmount { get; set; }
    public int? Total_Applied_Amount { get; set; }
    //Beviljat belopp:
    public int? Total_Granted_Amount { get; set; }
    public int? Total_Reported_Amount { get; set; }
    //Godkänt belopp:
    public int? Total_Approved_Amount { get; set; }
    public int? Applied_Amount_ExtraFunds { get; set; }
    public int? Granted_Amount_ExtraFunds { get; set; }
    public int? Reported_Amount_ExtraFunds { get; set; }
    public int? Approved_Adjusted_Amount_ExtraFunds { get; set; }
    public int? Applied_AuditGrant { get; set; }
    public int? Granted_AuditGrant { get; set; }
    public int? Applied_AdminGrant { get; set; }
    public int? Granted_AdminGrant { get; set; }
    public int? Applied_Amount_Scholarships { get; set; }
    public int? Granted_Amount_Scholarships { get; set; }
    public int? Approved_Amount_Scholarships { get; set; }

    [Required]
    public string Dnr { get; set; }
    }
}
