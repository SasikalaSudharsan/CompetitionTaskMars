using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    {
        LoginPage loginPageObj;
        HomePage homePageObj;
        EducationPage educationPageObj;
        public static ExtentReports extent;
        public static ExtentTest test;
        TestData testData;

        public Education_Tests()
        {
            loginPageObj = new LoginPage();
            homePageObj = new HomePage();
            educationPageObj = new EducationPage();
        }

        [OneTimeSetUp]
        public static void ExtentStart()
        {
            extent = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(@"D:\Sasikala\MVP_Studio\CompetitionTask\CompetitionTaskMars\CompetitionTaskMars\ExtentReports\Education.html");
            extent.AttachReporter(sparkReporter);
        }

        [OneTimeTearDown]
        public static void ExtentClose()
        {
            extent.Flush();
        }

        [SetUp]
        public void jsonSetUp()
        {
            string jsonFilePath = "D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars\\Data\\testdata.json";
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            testData = JsonConvert.DeserializeObject<TestData>(jsonContent);
        }

        [SetUp]
        public void loginSetUp()
        {
            Initialize();
            loginPageObj.LoginActions();
            homePageObj.GoToEducationPage();
        }

        [Test, Order(1), Description("This test is deleting all records in the education list")]
        public void Delete_All_Records()
        {
            test = extent.CreateTest("Delete_AllRecords").Info("Test started");
            educationPageObj.Delete_All_Records();
            test.Log(Status.Pass, "Delete_AllRecords passed");
        }

        [Test, Order(2), Description("This test is adding education in the list")]
        public void Add_Education()
        {
            test = extent.CreateTest("Add_Education").Info("Test started");
            educationPageObj.Add_Education(testData);
            
            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education has been added", "Actual message and expected message do not match");

            string newUniversityName = educationPageObj.getUniversityName(testData.UniversityName);
            string newCountry = educationPageObj.getCountry(testData.Country);
            string newTitle = educationPageObj.getTitle(testData.Title);
            string newDegree = educationPageObj.getDegree(testData.Degree);
            string newYearOfGraduation = educationPageObj.getYearOfGraduation(testData.YearOfGraduation);

            Assert.That(newUniversityName == testData.UniversityName, "Actual University name and expected University name do not match");
            Assert.That(newCountry == testData.Country, "Actual country and expected country do not match");
            Assert.That(newTitle == testData.Title, "Actual title and expected title do not match");
            Assert.That(newDegree == testData.Degree, "Actual degree and expected degree do not match");
            Assert.That(newYearOfGraduation == testData.YearOfGraduation, "Actual yearOfGraduation and expected yearOfGraduation do not match");

            test.Log(Status.Pass, "Add_Education passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(3), Description("This test is updating an existing education in the list")]
        public void Update_Education()
        {
            test = extent.CreateTest("Update_Education").Info("Test started");
            educationPageObj.Update_Education("University of London", "United Kingdom", "B.Tech", "Computer Science", "2015");

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education as been updated", "Actual message and expected message do not match");

            string updatedUniversityName = educationPageObj.getUniversityName("University of Austin");
            string updatedCountry = educationPageObj.getCountry("United States");
            string updatedTitle = educationPageObj.getTitle("B.A");
            string updatedDegree = educationPageObj.getDegree("English");
            string updatedYearOfGraduation = educationPageObj.getYearOfGraduation("2020");

            Assert.That(updatedUniversityName == "University of Austin", "Updated University name and expected University name do not match");
            Assert.That(updatedCountry == "United States", "Updated country and expected country do not match");
            Assert.That(updatedTitle == "B.A", "Updated title and expected title do not match");
            Assert.That(updatedDegree == "English", "Updated degree and expected degree do not match");
            Assert.That(updatedYearOfGraduation == "2020", "Updated yearOfGraduation and expected yearOfGraduation do not match");

            test.Log(Status.Pass, "Update_Education passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(4), Description("This test is deleting an existing education in the list")]
        public void Delete_Education()
        {
            test = extent.CreateTest("Delete_Education").Info("Test started");
            educationPageObj.Delete_Education("University of Austin", "United States", "B.A", "English", "2020");

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education entry successfully removed", "Actual message and expected message do not match");

            string deletedEducation = educationPageObj.getDeletedEducation("University of Austin", "United States", "B.A", "English", "2020");
            Assert.That(deletedEducation == null, "Expected education has not been deleted");

            test.Log(Status.Pass, "Delete_Education passed");
            Console.WriteLine(actualMessage);
        }

        [TearDown]
        public void TearDown()
        {
            Close();
        }


    }
}
