using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CompetitionTaskMars.Pages
{
    public class EducationPage : CommonDriver
    {
        private IReadOnlyCollection<IWebElement> deleteButtons => driver.FindElements(By.XPath("//div[@data-tab='third']//i[@class='remove icon']"));
        private IWebElement AddNewButton          => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
        private IWebElement UniversityNameTextbox => driver.FindElement(By.XPath("//input[@name='instituteName']"));
        private IWebElement CountryOfUniversity   => driver.FindElement(By.XPath("//select[@name='country']"));
        private IWebElement Title                 => driver.FindElement(By.XPath("//select[@name='title']"));
        private IWebElement Degree                => driver.FindElement(By.XPath("//input[@name='degree']"));
        private IWebElement YearOfGraduation      => driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
        private IWebElement AddButton             => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement SuccessMessage        => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private Func<string, IWebElement> newUniversityName = universityName => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{universityName}']"));
        private Func<string, IWebElement> newCountry = country => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{country}']"));
        private Func<string, IWebElement> newTitle = title => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{title}']"));
        private Func<string, IWebElement> newDegree = degree => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{degree}']"));
        private Func<string, IWebElement> newYearOfGraduation = yearOfGraduation => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Education']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{yearOfGraduation}']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//input[@value='Cancel']"));
        private IWebElement UpdateNewButton => driver.FindElement(By.XPath("//input[@value='Update']"));

        public void Delete_All_Records()
        {
            try
            {
                Wait.WaitToBeClickable("XPath", "//div[@data-tab='third']//i[@class='remove icon']", 4);
            }
            catch(WebDriverTimeoutException e)
            {
                return;
            }
            //Delete all records in the list
            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }
        }

        public void Add_Education(EducationData educationData)
        {
            //Click Add new button
            AddNewButton.Click();
            //Enter the UniversityName that needs to be added
            UniversityNameTextbox.SendKeys(educationData.UniversityName);
            //Choose the country
            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(educationData.Country);
            //Choose the title
            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(educationData.Title);
            //Enter the degree
            Degree.SendKeys(educationData.Degree);
            //Choose the yearOfGraduation
            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(educationData.YearOfGraduation);
            //Click Add button
            AddButton.Click();
        }

        public string getMessage()
        {
            // Thread.Sleep(4000);
            Wait.WaitToExist("XPath", "//div[@class='ns-box-inner']", 4);
            //Get the text message after adding education
            return SuccessMessage.Text;
        }

        public string getUniversityName(string universityName)
        {
            Thread.Sleep(4000);
            // Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CollegeName}']", 4);
            return newUniversityName(universityName).Text;
        }

        public string getCountry(string country)
        {
            Thread.Sleep(4000);
            // Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Country}']", 4);
            return newCountry(country).Text;
        }

        public string getTitle(string title)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Title}']", 4);
            return newTitle(title).Text;
        }

        public string getDegree(string degree)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Degree}']", 4);
            return newDegree(degree).Text;
        }

        public string getYearOfGraduation(string yearOfGraduation)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{GraduationYear}']", 4);
            return newYearOfGraduation(yearOfGraduation).Text;
        }

        public void getCancel()
        {
            try
            {
                //Click the cancel button
                CancelButton.Click();
            }
            catch(NoSuchElementException)
            {
                return;
            }
        }

        public void Update_Education(EducationData existingEducationData, EducationData newEducationData)
        {
            Thread.Sleep(4000);
            string xpath = $@"//div[@data-tab='third']//tr[td[1]='{existingEducationData.Country}' and td[2]='{existingEducationData.UniversityName}'" +
                                                        $" and td[3]='{existingEducationData.Title}' and td[4]='{existingEducationData.Degree}' and td[5]='{existingEducationData.YearOfGraduation}']/td[last()]/span[1]";
            IWebElement UpdateButton = driver.FindElement(By.XPath(xpath)); 
            //Click the update button
            UpdateButton.Click(); 
            //Clear and enter the university name that needs to be updated
            UniversityNameTextbox.Clear();
            UniversityNameTextbox.SendKeys(newEducationData.UniversityName);
            //Choose the country
            SelectElement chooseCountry = new SelectElement(CountryOfUniversity);
            chooseCountry.SelectByValue(newEducationData.Country);
            //Choose the title
            SelectElement chooseTitle = new SelectElement(Title);
            chooseTitle.SelectByValue(newEducationData.Title);
            //Clear and enter the degree that needs to be updated
            Degree.Clear();
            Degree.SendKeys(newEducationData.Degree);
            //Choose the yearOfGraduation
            SelectElement chooseYearOfGraduation = new SelectElement(YearOfGraduation);
            chooseYearOfGraduation.SelectByValue(newEducationData.YearOfGraduation);
            //Click the update button
            UpdateNewButton.Click();
        }

        public void Delete_Education(EducationData EducationData)
        {
            Thread.Sleep(4000);
            //Click the delete button that needs to be deleted
            string xpath = $@"//div[@data-tab='third']//tr[td[1]='{EducationData.Country}' and td[2]='{EducationData.UniversityName}'" +
                                                        $" and td[3]='{EducationData.Title}' and td[4]='{EducationData.Degree}' and td[5]='{EducationData.YearOfGraduation}']/td[last()]/span[2]";
            IWebElement DeleteButton = driver.FindElement(By.XPath(xpath));
            DeleteButton.Click();
        }

        public string getDeletedEducation(EducationData EducationData)
        {
            Thread.Sleep(4000);
            try
            {
                string xpath = $@"//div[@data-tab='third']//tr[td[1]='{EducationData.Country}' and td[2]='{EducationData.UniversityName}'" +
                                                        $" and td[3]='{EducationData.Title}' and td[4]='{EducationData.Degree}' and td[5]='{EducationData.YearOfGraduation}']";
                IWebElement DeletedEducation = driver.FindElement(By.XPath(xpath));
                return DeletedEducation.Text;
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
    }
}
