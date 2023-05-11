using Newtonsoft.Json;


namespace WebApplication5.Models
{
    public class TypeObjectDictionaryGeneration
    {

        public TypeObjectDictionaryGeneration()
        {
            GenerateDictionaryFile();
        }


        private void GenerateDictionaryFile()
        {
            Dictionary<Type, object> modelInstances = new Dictionary<Type, object>
                    {
                        { typeof(ApplicationAndEvaluation), new ApplicationAndEvaluation() },
                        { typeof(Organization), new Organization() },
                        { typeof(Participant), new Participant() },
                        { typeof(Payment), new Payment() },
                        { typeof(PreviousApplication), new PreviousApplication() },
                        { typeof(Program), new Program() },
                        { typeof(ReportAndReclaim), new ReportAndReclaim() },
                        { typeof(ScholarshipAndGrant), new ScholarshipAndGrant() }
                    };

            string jsonString = JsonConvert.SerializeObject(modelInstances);
            File.WriteAllText("objectlist.json", jsonString);
        }

    }
}
