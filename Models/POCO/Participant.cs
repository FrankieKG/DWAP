using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models.POCO
{
  public class Participant
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ParticipantId { get; set; }
    [MaxLength(255)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthData { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public string Level { get; set; }
    public int Applied_Participant_Number { get; set; }
    public int Granted_Participant_Number { get; set; }
    public int Reported_Participant_Number { get; set; }
    public int Applied_Staff_Teacher_Number { get; set; }
    public int Reported_Staff_Teacher_Number { get; set; }
    public int Applied_Student_Number { get; set; }
    public int Reported_Student_Number { get; set; }
    public int Reported_Women_Student_Number { get; set; }
    public int Reported_Men_Student_Number { get; set; }
    public int Reported_Women_Teacher_Number { get; set; }
    public int Reported_Men_Teacher_Number { get; set; }
    public int Reported_Women_SchoolLeader_Number { get; set; }
    public int Reported_Men_SchoolLeader_Number { get; set; }
    public int Reported_Women_AssociatedStaff_Number { get; set; }
    public int Reported_Men_AssociatedStaff_Number { get; set; }
    public int ApplicationId { get; set; }
    [ForeignKey(nameof(ApplicationId))]
    public virtual ApplicationAndEvaluation Application { get; set; }
  }
}
