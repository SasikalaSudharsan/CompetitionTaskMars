using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver;
        public static ExtentReports extent = new ExtentReports();
        public static ExtentTest test;

        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5000/Home");
        }

        public void CaptureScreenshot(string screenshotName)
        {
            // Capture the screenshot
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string filePath = "D:\\Sasikala\\MVP_Studio\\CompetitionTask\\CompetitionTaskMars\\CompetitionTaskMars\\Screenshot";
            string screenshotPath = Path.Combine(filePath, $"{screenshotName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(screenshotPath);
        }

        public bool ContainsSpecialCharacters(string universityName)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(universityName, @"[^a-zA-Z0-9\s]");
        }

        public void Close()
        {
            driver.Quit();
        }
    }
}
