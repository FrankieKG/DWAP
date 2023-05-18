

namespace WebApplication5.Models
{
    public class AtlasPraktikData
    {
        public string Dnr { get; set; }
        public string Period { get; set; }                              //ApplicationsAndEvaluations
        public string ApplicationStatus { get; set; }                   //ApplicationsAndEvaluations
        public int? Total_Granted_Amount { get; set; }                  //Payments
        public int? Total_Approved_Amount { get; set; }                 //Payments
        public int? Granted_Participant_Number { get; set; }            //Resten är från Participants
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
