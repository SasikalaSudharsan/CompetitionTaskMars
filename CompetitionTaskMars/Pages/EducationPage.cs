using CompetitionTaskMars.Utilities;
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
        private SelectElement dropdown;

        public void Add_Education()
        {
            IWebElement AddNewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
            AddNewButton.Click();

            IWebElement UniversityNameTextbox = driver.FindElement(By.XPath("//input[@name='instituteName']"));
            UniversityNameTextbox.SendKeys("University of London");

            IWebElement CountryOfUniversity = driver.FindElement(By.XPath("//select[@name='country']"));
            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue("United Kingdom");

            IWebElement Title = driver.FindElement(By.XPath("//select[@name='title']"));
            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue("B.Tech");

            IWebElement Degree = driver.FindElement(By.XPath("//input[@name='degree']"));
            Degree.SendKeys("Computer Science");

            IWebElement YearOfGraduation = driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue("2015");

            IWebElement AddButton = driver.FindElement(By.XPath("//input[@value='Add']"));
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
    }
}
