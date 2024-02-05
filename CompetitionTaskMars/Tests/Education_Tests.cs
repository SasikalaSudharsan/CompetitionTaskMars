using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;


namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Education_Tests : CommonDriver
    {
        LoginPage loginPageObj;
        HomePage homePageObj;
        EducationPage educationPageObj;
        EducationData educationData;
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
            //Create new instance of ExtentReports
            extent = new ExtentReports();
            //Create new instance of ExtentSparkReporter
            var sparkReporter = new ExtentSparkReporter(@"D:\Sasikala\MVP_Studio\CompetitionTask\CompetitionTaskMars\CompetitionTaskMars\ExtentReports\Education.html");
            //Attach the ExtentSparkReporter to the ExtentReports
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
            test = extent.CreateTest("Add_Education").Info("Test started");
            // Read test data for the AddEducation test case
            List<EducationData> educationDataList = EducationDataHelper.ReadEducationData(@"addEducationData.json");

            // Iterate through test data and retrieve AddEducation test data
            foreach (var educationData in educationDataList)
            {
                //Call the Add_Education method
                educationPageObj.Add_Education(educationData);
                string actualMessage = educationPageObj.getMessage();

                // Check if the UniversityName contains special characters
                bool containsSpecialChars = ContainsSpecialCharacters(educationData.UniversityName);
                if (containsSpecialChars)
                {
                    try
                    {
                        // Verify that the actual message matches the expected message for special characters
                        Assert.That(actualMessage == "Special characters are not allowed", "Actual message and expected message do not match");
                    }
                    catch (AssertionException ex)
                    {
                        // Log the failure and capture a screenshot
                        test.Log(Status.Fail, "Education failed: " + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("SpecialCharsEducationFailed");
                    }
                }
                else
                {
                    // Verify if special characters are not present in the UniversityName
                    if (educationPageObj.getUniversityName(educationData.UniversityName) == educationData.UniversityName)
                    {
                        Assert.That(educationPageObj.getUniversityName(educationData.UniversityName) == educationData.UniversityName, "Actual University name and expected University name do not match");
                        Assert.That(educationPageObj.getCountry(educationData.Country) == educationData.Country, "Actual country and expected country do not match");
                        Assert.That(educationPageObj.getTitle(educationData.Title) == educationData.Title, "Actual title and expected title do not match");
                        Assert.That(educationPageObj.getDegree(educationData.Degree) == educationData.Degree, "Actual degree and expected degree do not match");
                        Assert.That(educationPageObj.getYearOfGraduation(educationData.YearOfGraduation) == educationData.YearOfGraduation, "Actual yearOfGraduation and expected yearOfGraduation do not match");
                        Console.WriteLine(actualMessage);
                    }
                    try
                    {
                        Assert.That(actualMessage == "Education has been added" || actualMessage == "This information is already exist.", "Actual message and expected message do not match");
                        test.Log(Status.Pass, "Education passed");
                        // If information already exists, call the cancel method
                        if (actualMessage == "This information is already exist.")
                        {
                            educationPageObj.getCancel();
                        }
                    }
                    catch (AssertionException ex)
                    {
                        // Log the failure and capture a screenshot
                        test.Log(Status.Fail, "Education failed: " + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("EducationTestFailed");
                    }
                }
            }
        }

        [Test, Order(3), Description("This test is updating an existing education in the list")]
        [TestCase(1)]
        public void Update_Education(int id)
        {
            test = extent.CreateTest("Update_Education").Info("Test started");

            // Read education data from the specified JSON file and retrieve the item with a matching Id
            EducationData existingEducationData = EducationDataHelper.ReadEducationData(@"addEducationData.json").FirstOrDefault(x => x.Id == id);
            EducationData newEducationData = EducationDataHelper.ReadEducationData(@"updateEducationData.json").FirstOrDefault(x => x.Id == id);
            
            educationPageObj.Update_Education(existingEducationData, newEducationData);
            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education as been updated", "Actual message and expected message do not match");

            Assert.That(educationPageObj.getUniversityName(newEducationData.UniversityName) == newEducationData.UniversityName, "Updated University name and expected University name do not match");
            Assert.That(educationPageObj.getCountry(newEducationData.Country) == newEducationData.Country, "Updated country and expected country do not match");
            Assert.That(educationPageObj.getTitle(newEducationData.Title) == newEducationData.Title, "Updated title and expected title do not match");
            Assert.That(educationPageObj.getDegree(newEducationData.Degree) == newEducationData.Degree, "Updated degree and expected degree do not match");
            Assert.That(educationPageObj.getYearOfGraduation(newEducationData.YearOfGraduation) == newEducationData.YearOfGraduation, "Updated yearOfGraduation and expected yearOfGraduation do not match");
            test.Log(Status.Pass, "Update_Education passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(4), Description("This test is deleting an existing education in the list")]
        [TestCase(1)]
        public void Delete_Education(int id)
        {
            test = extent.CreateTest("Delete_Education").Info("Test started");

            // Read education data from the specified JSON file and retrieve the item with a matching Id
            EducationData educationData = EducationDataHelper.ReadEducationData(@"deleteEducationData.json").FirstOrDefault(x => x.Id == id);
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
            test = extent.CreateTest("EmptyTextbox_Education").Info("Test started");
            // Read test data for the emptyEducation test case
            List<EducationData> educationDataList = EducationDataHelper.ReadEducationData(@"emptyEducationData.json");

            // Iterate through test data and retrieve EmptyEducation test data
            foreach (var educationData in educationDataList)
            {
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
            //Flush the ExtentReports instance
            extent.Flush();
        }
    }
}
