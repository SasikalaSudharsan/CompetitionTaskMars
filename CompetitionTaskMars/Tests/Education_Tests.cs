using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
        public void LoginSetUp()
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
            // Read test data for the AddEducation test case
            var testDataList = TestDataHelper.ReadTestData("D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars\\Data\\testdata.json")["AddEducation"];

            // Iterate through test data and retrieve AddEducation test data
            foreach (var testData in testDataList)
            {
                string universityName = testData["UniversityName"];
                string country = testData["Country"];
                string title = testData["Title"];
                string degree = testData["Degree"];
                string yearOfGraduation = testData["YearOfGraduation"];

                test = extent.CreateTest("Add_Education").Info("Test started");
                educationPageObj.Add_Education(universityName, country, title, degree, yearOfGraduation);

                string actualMessage = educationPageObj.getMessage();
                Assert.That(actualMessage == "Education has been added", "Actual message and expected message do not match");

                string newUniversityName = educationPageObj.getUniversityName(universityName);
                string newCountry = educationPageObj.getCountry(country);
                string newTitle = educationPageObj.getTitle(title);
                string newDegree = educationPageObj.getDegree(degree);
                string newYearOfGraduation = educationPageObj.getYearOfGraduation(yearOfGraduation);

                Assert.That(newUniversityName == universityName, "Actual University name and expected University name do not match");
                Assert.That(newCountry == country, "Actual country and expected country do not match");
                Assert.That(newTitle == title, "Actual title and expected title do not match");
                Assert.That(newDegree == degree, "Actual degree and expected degree do not match");
                Assert.That(newYearOfGraduation == yearOfGraduation, "Actual yearOfGraduation and expected yearOfGraduation do not match");

                test.Log(Status.Pass, "Add_Education passed");
                Console.WriteLine(actualMessage);
            }
        }

        [Test, Order(3), Description("This test is updating an existing education in the list")]
        public void Update_Education()
        {
            var testDataList = TestDataHelper.ReadTestData("D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars\\Data\\testdata.json")["UpdateEducation"];

            foreach (var testData in testDataList)
            {
                string existingUniversityName = testData["ExistingUniversityName"];
                string existingCountry = testData["ExistingCountry"];
                string existingTitle = testData["ExistingTitle"];
                string existingDegree = testData["ExistingDegree"];
                string existingYearOfGraduation = testData["ExistingYearOfGraduation"];
                string newUniversityName = testData["NewUniversityName"];
                string newCountry = testData["NewCountry"];
                string newTitle = testData["NewTitle"];
                string newDegree = testData["NewDegree"];
                string newYearOfGraduation = testData["NewYearOfGraduation"];

                test = extent.CreateTest("Update_Education").Info("Test started");
                educationPageObj.Update_Education(existingUniversityName, existingCountry, existingTitle, existingDegree, existingYearOfGraduation, newUniversityName,
                                                           newCountry, newTitle, newDegree, newYearOfGraduation);

                string actualMessage = educationPageObj.getMessage();
                Assert.That(actualMessage == "Education as been updated", "Actual message and expected message do not match");

                string updatedUniversityName = educationPageObj.getUniversityName(newUniversityName);
                string updatedCountry = educationPageObj.getCountry(newCountry);
                string updatedTitle = educationPageObj.getTitle(newTitle);
                string updatedDegree = educationPageObj.getDegree(newDegree);
                string updatedYearOfGraduation = educationPageObj.getYearOfGraduation(newYearOfGraduation);

                Assert.That(updatedUniversityName == newUniversityName, "Updated University name and expected University name do not match");
                Assert.That(updatedCountry == newCountry, "Updated country and expected country do not match");
                Assert.That(updatedTitle == newTitle, "Updated title and expected title do not match");
                Assert.That(updatedDegree == newDegree, "Updated degree and expected degree do not match");
                Assert.That(updatedYearOfGraduation == newYearOfGraduation, "Updated yearOfGraduation and expected yearOfGraduation do not match");

                test.Log(Status.Pass, "Update_Education passed");
                Console.WriteLine(actualMessage);
            }
        }

        [Test, Order(4), Description("This test is deleting an existing education in the list")]
        public void Delete_Education()
        {
            var testDataList = TestDataHelper.ReadTestData("D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars\\Data\\testdata.json")["DeleteEducation"];
            foreach (var testData in testDataList)
            {
                string universityName = testData["UniversityName"];
                string country = testData["Country"];
                string title = testData["Title"];
                string degree = testData["Degree"];
                string yearOfGraduation = testData["YearOfGraduation"];

                test = extent.CreateTest("Delete_Education").Info("Test started");
                educationPageObj.Delete_Education(universityName, country, title, degree, yearOfGraduation);

                string actualMessage = educationPageObj.getMessage();
                Assert.That(actualMessage == "Education entry successfully removed", "Actual message and expected message do not match");

                string deletedEducation = educationPageObj.getDeletedEducation(universityName, country, title, degree, yearOfGraduation);
                Assert.That(deletedEducation == null, "Expected education has not been deleted");

                test.Log(Status.Pass, "Delete_Education passed");
                Console.WriteLine(actualMessage);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Close();
        }


    }
}
