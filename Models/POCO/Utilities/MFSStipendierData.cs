namespace WebApplication5.Models.POCO.Utilities
{
    public class MFSStipendierData
    {
        //ApplicationsAndEvaluations
        public string Dnr { get; set; }
        public string Period { get; set; }
        public string ApplicationStatus { get; set; }
        //Payments
        public int? Total_Granted_Amount { get; set; }
        public int? Total_Approved_Amount { get; set; }


    }
}
