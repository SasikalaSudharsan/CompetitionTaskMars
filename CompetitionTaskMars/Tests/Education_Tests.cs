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
using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Selenium = OpenQA.Selenium;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    {
        LoginPage loginPageObj;
        HomePage homePageObj;
        EducationPage educationPageObj;
        EducationData educationData;

        public Education_Tests()
        {
            loginPageObj = new LoginPage();
            homePageObj = new HomePage();
            educationPageObj = new EducationPage();
        }

        [OneTimeSetUp]
        public static void ExtentStart()
        {
            var sparkReporter = new ExtentSparkReporter(@"D:\Sasikala\MVP_Studio\CompetitionTask\CompetitionTaskMars\CompetitionTaskMars\ExtentReports\Education.html");
            extent.AttachReporter(sparkReporter);
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
            List<EducationData> educationDataList = EducationDataHelper.ReadEducationData(@"addEducationData.json");

            // Iterate through test data and retrieve AddEducation test data
            foreach (var educationData in educationDataList)
            {
                test = extent.CreateTest("Add_Education").Info("Test started");
                educationPageObj.Add_Education(educationData);

                bool containsSpecialChars = ContainsSpecialCharacters(educationData.UniversityName);

                string actualMessage = educationPageObj.getMessage();

                if (containsSpecialChars)
                {
                    try
                    {
                        Assert.That(actualMessage == "Special characters are not allowed", "Actual message and expected message do not match");
                    }
                    catch (AssertionException ex)
                    {
                        test.Log(Status.Fail, "Education failed: " + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("SpecialCharactersFailed");
                    }
                }
                else
                {
                    if (educationPageObj.getUniversityName(educationData.UniversityName) == educationData.UniversityName)
                    {
                        Assert.That(educationPageObj.getUniversityName(educationData.UniversityName) == educationData.UniversityName, "Actual University name and expected University name do not match");
                        Assert.That(educationPageObj.getCountry(educationData.Country) == educationData.Country, "Actual country and expected country do not match");
                        Assert.That(educationPageObj.getTitle(educationData.Title) == educationData.Title, "Actual title and expected title do not match");
                        Assert.That(educationPageObj.getDegree(educationData.Degree) == educationData.Degree, "Actual degree and expected degree do not match");
                        Assert.That(educationPageObj.getYearOfGraduation(educationData.YearOfGraduation) == educationData.YearOfGraduation, "Actual yearOfGraduation and expected yearOfGraduation do not match");
                        test.Log(Status.Pass, "Add_Education passed");
                        Console.WriteLine(actualMessage);
                    }
                    try
                    {
                        Assert.That(actualMessage == "Education has been added" || actualMessage == "This information is already exist.", "Actual message and expected message do not match");
                        if (actualMessage == "This information is already exist.")
                        {
                            // Call the cancel() method or perform the necessary action
                            educationPageObj.getCancel();
                        }
                        test.Log(Status.Pass, "Education passed");
                    }
                    catch (AssertionException ex)
                    {
                        test.Log(Status.Fail, "Education failed: " + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("TestFailed");
                    }
                }
            }
        }

        [Test, Order(3), Description("This test is updating an existing education in the list")]
        [TestCase(1)]
        public void Update_Education(int id)
        {
            EducationData existingEducationData = EducationDataHelper.ReadEducationData(@"addEducationData.json").FirstOrDefault(x => x.Id == id);
            EducationData newEducationData = EducationDataHelper.ReadEducationData(@"updateEducationData.json").FirstOrDefault(x => x.Id == id);

            test = extent.CreateTest("Update_Education").Info("Test started");
            educationPageObj.Update_Education(existingEducationData, newEducationData);

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education as been updated", "Actual message and expected message do not match");

            string updatedUniversityName = educationPageObj.getUniversityName(newEducationData.UniversityName);
            string updatedCountry = educationPageObj.getCountry(newEducationData.Country);
            string updatedTitle = educationPageObj.getTitle(newEducationData.Title);
            string updatedDegree = educationPageObj.getDegree(newEducationData.Degree);
            string updatedYearOfGraduation = educationPageObj.getYearOfGraduation(newEducationData.YearOfGraduation);

            Assert.That(updatedUniversityName == newEducationData.UniversityName, "Updated University name and expected University name do not match");
            Assert.That(updatedCountry == newEducationData.Country, "Updated country and expected country do not match");
            Assert.That(updatedTitle == newEducationData.Title, "Updated title and expected title do not match");
            Assert.That(updatedDegree == newEducationData.Degree, "Updated degree and expected degree do not match");
            Assert.That(updatedYearOfGraduation == newEducationData.YearOfGraduation, "Updated yearOfGraduation and expected yearOfGraduation do not match");

            test.Log(Status.Pass, "Update_Education passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(4), Description("This test is deleting an existing education in the list")]
        [TestCase(1)]
        public void Delete_Education(int id)
        {
            EducationData educationData = EducationDataHelper.ReadEducationData(@"deleteEducationData.json").FirstOrDefault(x => x.Id == id);
            test = extent.CreateTest("Delete_Education").Info("Test started");
            educationPageObj.Delete_Education(educationData);

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education entry successfully removed", "Actual message and expected message do not match");

            string deletedEducation = educationPageObj.getDeletedEducation(educationData);
            Assert.That(deletedEducation == null, "Expected education has not been deleted");

            test.Log(Status.Pass, "Delete_Education passed");
            Console.WriteLine(actualMessage);
        }
                
        [Test, Order(5), Description("This test is adding empty textbox in the education list")]
        public void EmptyTextbox_Education()
        {
            // Read test data for the AddEducation test case
            List<EducationData> educationDataList = EducationDataHelper.ReadEducationData(@"emptyEducationData.json");

            // Iterate through test data and retrieve EmptyEducation test data
            foreach (var educationData in educationDataList)
            {
                test = extent.CreateTest("EmptyTextbox_Education").Info("Test started");
                educationPageObj.Add_Education(educationData);

                string actualMessage = educationPageObj.getMessage();
                Assert.That(actualMessage == "Please enter all the fields", "Actual message and expected message do not match");

                test.Log(Status.Pass, "EmptyTextbox_Education passed");
                Console.WriteLine(actualMessage);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Close();
        }

        [OneTimeTearDown]
        public static void ExtentClose()
        {
            extent.Flush();
        }
    }
}
