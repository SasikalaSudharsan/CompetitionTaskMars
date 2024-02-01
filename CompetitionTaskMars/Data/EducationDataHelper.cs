using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Data
{
    public class EducationDataHelper
    {
        public static List<EducationData> ReadEducationData(string jsonFileName)
        {
            string currentDirectory = "D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars";
            string filePath = Path.Combine(currentDirectory, "Data", jsonFileName);
            string jsonContent = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<EducationData>>(jsonContent) ?? new List<EducationData>();
        }
    }
}
