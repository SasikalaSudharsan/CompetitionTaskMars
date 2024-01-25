using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Data
{
    public class TestDataHelper
    {
        public static Dictionary<string, List<Dictionary<string, string>>> ReadTestData(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(jsonContent);
        }
    }
}
