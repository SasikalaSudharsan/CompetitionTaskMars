using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Data
{
    public class CertificationDataHelper
    {
        public static List<CertificationData> ReadCertificationData(string jsonFileName)
        {
            string currentDirectory = "D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars";
            string filePath = Path.Combine(currentDirectory, "Data", jsonFileName);
            string jsonContent = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<CertificationData>>(jsonContent) ?? new List<CertificationData>();
        }
    }
}
