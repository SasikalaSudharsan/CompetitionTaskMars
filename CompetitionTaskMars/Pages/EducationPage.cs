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
        private IWebElement AddNewButton          => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
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

        public void Add_Education(EducationData educationData)
        {
            AddNewButton.Click();
            Thread.Sleep(4000);
            UniversityNameTextbox.Clear();
            UniversityNameTextbox.SendKeys(Keys.Control + "A");
            UniversityNameTextbox.SendKeys(Keys.Backspace);
            UniversityNameTextbox.SendKeys(educationData.UniversityName);
            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(educationData.Country);
            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(educationData.Title);
            Degree.SendKeys(educationData.Degree);
            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(educationData.YearOfGraduation);
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

        public void getCancel()
        {
            IWebElement CancelButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//input[@value='Cancel']"));
            CancelButton.Click();
        }

        public void Update_Education(EducationData existingEducationData, EducationData newEducationData)
        {
            Thread.Sleep(4000);
            IWebElement UpdateButton = driver.FindElement(By.XPath($"//div[@data-tab='third']//tr[td[1]='{existingEducationData.Country}' and td[2]='{existingEducationData.UniversityName}'" +
                                                        $" and td[3]='{existingEducationData.Title}' and td[4]='{existingEducationData.Degree}' and td[5]='{existingEducationData.YearOfGraduation}']/td[last()]/span[1]"));
            UpdateButton.Click();
            
            UniversityNameTextbox.Clear();
            UniversityNameTextbox.SendKeys(newEducationData.UniversityName);

            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(newEducationData.Country);

            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(newEducationData.Title);

            Degree.Clear();
            Degree.SendKeys(newEducationData.Degree);

            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(newEducationData.YearOfGraduation);

            IWebElement UpdateNewButton = driver.FindElement(By.XPath("//input[@value='Update']"));
            UpdateNewButton.Click();
        }

        public void Delete_Education(EducationData EducationData)
        {
            Thread.Sleep(4000);

            IWebElement DeleteButton = driver.FindElement(By.XPath($"//div[@data-tab='third']//tr[td[1]='{EducationData.Country}' and td[2]='{EducationData.UniversityName}'" +
                                                        $" and td[3]='{EducationData.Title}' and td[4]='{EducationData.Degree}' and td[5]='{EducationData.YearOfGraduation}']/td[last()]/span[2]"));
            DeleteButton.Click();
        }

        public string getDeletedEducation(EducationData EducationData)
        {
            try
            {
                IWebElement DeletedEducation = driver.FindElement(By.XPath($"//div[@data-tab='third']//tr[td[1]='{EducationData.Country}' and td[2]='{EducationData.UniversityName}'" +
                                                        $" and td[3]='{EducationData.Title}' and td[4]='{EducationData.Degree}' and td[5]='{EducationData.YearOfGraduation}']"));
                return DeletedEducation.Text;
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
    }
}
