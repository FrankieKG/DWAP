using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
  public class Payment
  {
    [Key]
    public int PaymentId { get; set; }
    
    public float PaymentAmount { get; set; }
    public float Total_Applied_Amount { get; set; }
    public float Total_Granted_Amount { get; set; }
    public float Total_Reported_Amount { get; set; }
    public float Total_Approved_Amount { get; set; }
    public float Applied_Amount_ExtraFunds { get; set; }
    public float Granted_Amount_ExtraFunds { get; set; }
    public float Reported_Amount_ExtraFunds { get; set; }
    public float Approved_Adjusted_Amount_ExtraFunds { get; set; }
    public float Applied_AuditGrant { get; set; }
    public float Granted_AuditGrant { get; set; }
    public float Applied_AdminGrant { get; set; }
    public float Granted_AdminGrant { get; set; }
    public float Applied_Amount_Scholarships { get; set; }
    public float Granted_Amount_Scholarships { get; set; }
    public float Approved_Amount_Scholarships { get; set; }
    public int ApplicationId { get; set; }
  }
}
