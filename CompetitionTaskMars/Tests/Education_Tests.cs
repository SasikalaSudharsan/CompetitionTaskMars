using AventStack.ExtentReports;
using CompetitionTaskMars.Pages;
using CompetitionTaskMars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Tests
{
    public class Education_Tests : CommonDriver
    {
        LoginPage loginPageObj;
        HomePage homePageObj;
        EducationPage educationPageObj;
        ExtentReports extent;

        public Education_Tests()
        {
            loginPageObj = new LoginPage();
            homePageObj = new HomePage();
            educationPageObj = new EducationPage();
        }

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5000/Home");
            loginPageObj.LoginActions();            
            homePageObj.GoToEducationPage();
        }

        [Test]
        public void Add_Education()
        {
           educationPageObj.Add_Education();

            string actualMessage = educationPageObj.getMessage();
            Assert.That(actualMessage == "Education has been added", "Actual message and expected message do not match");

            string newUniversityName = educationPageObj.getUniversityName("University of London");
            string newCountry = educationPageObj.getCountry("United Kingdom");
            string newTitle = educationPageObj.getTitle("B.Tech");
            string newDegree = educationPageObj.getDegree("Computer Science");
            string newYearOfGraduation = educationPageObj.getYearOfGraduation("2015");

            Assert.That(newUniversityName == "University of London", "Actual University name and expected University name do not match");
            Assert.That(newCountry == "United Kingdom", "Actual country and expected country do not match");
            Assert.That(newTitle == "B.Tech", "Actual title and expected title do not match");
            Assert.That(newDegree == "Computer Science", "Actual degree and expected degree do not match");
            Assert.That(newYearOfGraduation == "2015", "Actual yearOfGraduation and expected yearOfGraduation do not match");

            Console.WriteLine(actualMessage);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
