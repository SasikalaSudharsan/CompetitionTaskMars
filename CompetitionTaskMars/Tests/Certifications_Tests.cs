using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionTaskMars.Data;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;

namespace CompetitionTaskMars.Tests
{
    [TestFixture]
    public class Certifications_Tests : CommonDriver
    {
        LoginPage loginPageObj;
        HomePage homePageObj;
        CertificationsPage certificationsPageObj;
        public static ExtentReports extent;
        public static ExtentTest test;

        public Certifications_Tests()
        {
            loginPageObj = new LoginPage();
            homePageObj = new HomePage();
            certificationsPageObj = new CertificationsPage();
        }

        [OneTimeSetUp]
        public static void ExtentStart()
        {
            //Create new instance of ExtentReports
            extent = new ExtentReports();
            //Create new instance of ExtentSparkReporter
            var sparkReporter = new ExtentSparkReporter(@"D:\Sasikala\MVP_Studio\CompetitionTask\CompetitionTaskMars\CompetitionTaskMars\ExtentReports\Certification.html");
            //Attach the ExtentSparkReporter to the ExtentReports
            extent.AttachReporter(sparkReporter);
        } 

        [SetUp]
        public void SetUp()
        {
            Initialize();
            loginPageObj.LoginActions();
            homePageObj.GoToCertificationsPage();
        }

        [Test, Order(1), Description("This test is deleting all records in the certification list")]
        public void Delete_All_Records()
        {
            test = extent.CreateTest("Delete_AllRecords").Info("Test started");
            certificationsPageObj.Delete_All_Records();
            test.Log(Status.Pass, "Delete_AllRecords passed");
        }

        [Test, Order(2), Description("This test is adding certification in the list")]
        public void Add_Certification()
        {
            test = extent.CreateTest("Add_Certification").Info("Test started");
            // Read test data for the AddCertification test case
            List<CertificationData> certificationDataList = CertificationDataHelper.ReadCertificationData(@"addCertificationData.json");

            // Iterate through test data and retrieve AddCertification test data
            foreach (var certificationData in certificationDataList)
            {
                certificationsPageObj.Add_Certification(certificationData);
                string actualMessage = certificationsPageObj.getMessage();

                // Check if the Certificate contains special characters
                bool containsSpecialChars = ContainsSpecialCharacters(certificationData.Certificate);
                if(containsSpecialChars)
                {
                    try
                    {
                        // Verify that the actual message matches the expected message for special characters
                        Assert.That(actualMessage == "Special characters are not allowed", "Actual message and expected message do not match");
                    }
                    catch(AssertionException ex) 
                    {
                        // Log the failure and capture a screenshot
                        test.Log(Status.Fail, "SpecialCharacters Certification failed" + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("SpecialCharsCertificationFailed");
                    }
                }
                else
                {
                    // Verify if special characters are not present in the Certificate
                    if (certificationsPageObj.getCertificate(certificationData.Certificate) == certificationData.Certificate)
                    {
                        Assert.That(certificationsPageObj.getCertificate(certificationData.Certificate) == certificationData.Certificate, "Actual certificate and expected certificate do not match");
                        Assert.That(certificationsPageObj.getCertifiedFrom(certificationData.CertifiedFrom) == certificationData.CertifiedFrom, "Actual certifiedFrom and expected certifiedFrom do not match");
                        Assert.That(certificationsPageObj.getYear(certificationData.Year) == certificationData.Year, "Actual year and expected year do not match");
                        Console.WriteLine(actualMessage);
                    }
                    try
                    {
                        Assert.That(actualMessage == "ISTQB has been added to your certification"  || actualMessage == "This information is already exist.", "Actual message and expected message do not match");
                        test.Log(Status.Pass, "Certification passed");
                        // If information already exists, call the cancel method
                        if (actualMessage == "This information is already exist.")
                        {
                            certificationsPageObj.getCancel();
                        }
                    }
                    catch(AssertionException ex)
                    {
                        // Log the failure and capture a screenshot
                        test.Log(Status.Fail, "Certification failed: " + ex.Message);
                        Console.WriteLine(actualMessage);
                        CaptureScreenshot("CertificationTestFailed");
                    }
                }              
            }
        }

        [Test, Order(3), Description("This test is updating an existing certification in the list")]
        [TestCase(1)]
        public void Update_Certification(int id)
        {
            test = extent.CreateTest("Update_Certification").Info("Test started");

            // Read certification data from the specified JSON file and retrieve the item with a matching Id
            CertificationData existingCertificationData = CertificationDataHelper.ReadCertificationData(@"addCertificationData.json").FirstOrDefault(x => x.Id == id);
            CertificationData newCertificationData = CertificationDataHelper.ReadCertificationData(@"updateCertificationData.json").FirstOrDefault(x => x.Id == id);

            certificationsPageObj.Update_Certification(existingCertificationData, newCertificationData);
            string actualMessage = certificationsPageObj.getMessage();
            Assert.That(actualMessage == "Microsoft Azure has been updated to your certification", "Actual message and expected message do not match");

            Assert.That(certificationsPageObj.getCertificate(newCertificationData.Certificate) == newCertificationData.Certificate, "Actual certificate and expected certificate do not match");
            Assert.That(certificationsPageObj.getCertifiedFrom(newCertificationData.CertifiedFrom) == newCertificationData.CertifiedFrom, "Actual certifiedFrom and expected certifiedFrom do not match");
            Assert.That(certificationsPageObj.getYear(newCertificationData.Year) == newCertificationData.Year, "Actual year and expected year do not match");
            test.Log(Status.Pass, "Update_Certification passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(4), Description("This test is deleting an existing certification in the list")]
        [TestCase(1)]
        public void Delete_Certification(int id)
        {
            test = extent.CreateTest("Delete_Certification").Info("Test started");

            // Read certification data from the specified JSON file and retrieve the item with a matching Id
            CertificationData certificationData = CertificationDataHelper.ReadCertificationData(@"deleteCertificationData.json").FirstOrDefault(x => x.Id == id);

            certificationsPageObj.Delete_Certification(certificationData);
            string actualMessage = certificationsPageObj.getMessage();
            Assert.That(actualMessage == "Microsoft Azure has been deleted from your certification", "Actual message and expected message do not match");

            Assert.That(certificationsPageObj.getDeletedCertification(certificationData) == null, "Expected certification has not been deleted");
            test.Log(Status.Pass, "Delete_Certification passed");
            Console.WriteLine(actualMessage);
        }

        [Test, Order(5), Description("This test is adding empty textbox in the certification list")]
        public void EmptyTextbox_Certification()
        {
            test = extent.CreateTest("EmptyTextbox_Certification").Info("Test started");
            // Read test data for the emptyCertification test case
            List<CertificationData> certificationDataList = CertificationDataHelper.ReadCertificationData(@"emptyCertificationData.json");

            // Iterate through test data and retrieve EmptyCertification test data
            foreach (var certificationData in certificationDataList)
            {
                certificationsPageObj.Add_Certification(certificationData);
                string actualMessage = certificationsPageObj.getMessage();
                Assert.That(actualMessage == "Please enter Certification Name, Certification From and Certification Year", "Actual message and expected message do not match");
                test.Log(Status.Pass, "EmptyTextbox_Certification passed");
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
