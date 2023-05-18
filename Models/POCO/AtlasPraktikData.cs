

namespace WebApplication5.Models
{
    public class AtlasPraktikData
    {
        public string Dnr { get; set; }
        public string Period { get; set; }                              
        public string ApplicationStatus { get; set; }                   
        public int? Total_Granted_Amount { get; set; }                  
        public int? Total_Approved_Amount { get; set; }                 
        public int? Granted_Participant_Number { get; set; }            
        public int? Reported_Participant_Number { get; set; }
        public int? Reported_Women_Student_Number { get; set; }
        public int? Reported_Men_Student_Number { get; set; }
        public int? Reported_Women_Teacher_Number { get; set; }
        public int? Reported_Men_Teacher_Number { get; set; }
        public int? Reported_Women_SchoolLeader_Number { get; set; }
        public int? Reported_Men_SchoolLeader_Number { get; set; }
        public int? Reported_Women_AssociatedStaff_Number { get; set; }
        public int? Reported_Men_AssociatedStaff_Number { get; set; }
    }
}
