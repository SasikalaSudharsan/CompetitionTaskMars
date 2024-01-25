using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Data
{
    public class TestDataReader
    {
        public static TestData ReadTestData(string jsonFilePath, string testName)
        {
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

            var testDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, TestData>>(jsonContent);
            if (testDataDictionary.ContainsKey(testName))
            {
                return testDataDictionary[testName];
            }
            else
            {
                // Handle the case where the test name is not found
                throw new KeyNotFoundException($"Test data not found for test: {testName}");
            }
        }
    }
}
