using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class EducationPage : CommonDriver
    {
        private IWebElement UniversityNameTextbox => driver.FindElement(By.XPath("//input[@name='instituteName']"));
        private IWebElement CountryOfUniversity   => driver.FindElement(By.XPath("//select[@name='country']"));
        private IWebElement Title                 => driver.FindElement(By.XPath("//select[@name='title']"));
        private IWebElement Degree                => driver.FindElement(By.XPath("//input[@name='degree']"));
        private IWebElement YearOfGraduation      => driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
        private IWebElement AddButton             => driver.FindElement(By.XPath("//input[@value='Add']"));

        public void Delete_All_Records()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> deleteButtons = driver.FindElements(By.XPath("//div[@data-tab='third']//i[@class='remove icon']"));
            //Delete all records in the list
            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }
        }

        public void Add_Education(TestData testData)
        {
            IWebElement AddNewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
            AddNewButton.Click();

            UniversityNameTextbox.SendKeys(testData.UniversityName);

            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(testData.Country);

            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(testData.Title);

            Degree.SendKeys(testData.Degree);

            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(testData.YearOfGraduation);

            AddButton.Click();
        }

        public string getMessage()
        {
            Thread.Sleep(4000);
            IWebElement SuccessMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            //Get the text message after entering language and language level
            return SuccessMessage.Text;
        }

        public string getUniversityName(string universityName)
        {
            Thread.Sleep(4000);
            IWebElement newUniversityName = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{universityName}']"));
            return newUniversityName.Text;
        }

        public string getCountry(string country)
        {
            Thread.Sleep(4000);
            IWebElement newCountry = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{country}']"));
            return newCountry.Text;
        }

        public string getTitle(string title)
        {
            Thread.Sleep(4000);
            IWebElement newTitle = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{title}']"));
            return newTitle.Text;
        }

        public string getDegree(string degree)
        {
            Thread.Sleep(4000);
            IWebElement newDegree = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{degree}']"));
            return newDegree.Text;
        }

        public string getYearOfGraduation(string yearOfGraduation)
        {
            Thread.Sleep(4000);
            IWebElement newYearOfGraduation = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{yearOfGraduation}']"));
            return newYearOfGraduation.Text;
        }

        public void Update_Education(TestData testData)
        {
            Thread.Sleep(4000);
            IWebElement UpdateButton = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3[text()='Education']]/following-sibling::div[@class='twelve wide column scrollTable']//tr[1][td[text()='{testData.ExistingUniversityname}'] " +
                $"and td[text()='{testData.ExistingCountry}'] and td[text()='{testData.ExistingTitle}'] and td[text()='{testData.ExistingDegree}'] and td[text()='{testData.ExistingYearofGraduation}']]//span[1]"));
            UpdateButton.Click();
            
            UniversityNameTextbox.Clear();
            UniversityNameTextbox.SendKeys(testData.NewUniversityName);

            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(testData.NewCountry);

            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(testData.NewTitle);

            Degree.Clear();
            Degree.SendKeys(testData.NewDegree);

            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(testData.NewYearofGraduation);

            IWebElement UpdateNewButton = driver.FindElement(By.XPath("//input[@value='Update']"));
            UpdateNewButton.Click();
        }

        public void Delete_Education(TestData testData)
        {
            Thread.Sleep(4000);

            IWebElement DeleteButton = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3[text()='Education']]/following-sibling::div[@class='twelve wide column scrollTable']//tr[1][td[text()='{testData.UniversityName}'] " +
                $"and td[text()='{testData.Country}'] and td[text()='{testData.Title}'] and td[text()='{testData.Degree}'] and td[text()='{testData.YearOfGraduation}']]//span[2]"));
            DeleteButton.Click();
        }

        public string getDeletedEducation(TestData testData)
        {
            try
            {
                IWebElement DeletedEducation = driver.FindElement(By.XPath($"\r\n\r\n//div[@class='four wide column' and h3[text()='Education']]/following-sibling::div[@class='twelve wide column scrollTable']//tr[1][td[text()='{testData.UniversityName}'] " +
                    $"and td[text()='{testData.Country}'] and td[text()='{testData.Title}'] and td[text()='{testData.Degree}'] and td[text()='{testData.YearOfGraduation}']]"));
                return DeletedEducation.Text;
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
    }
}
