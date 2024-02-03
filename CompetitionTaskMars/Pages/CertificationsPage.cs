using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CompetitionTaskMars.Pages
{
    public class CertificationsPage : CommonDriver
    {
        private IReadOnlyCollection<IWebElement> deleteButtons => driver.FindElements(By.XPath("//div[@data-tab='fourth']//i[@class='remove icon']"));
        private IWebElement AddNewButton         => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
        private IWebElement CertificateTextbox   => driver.FindElement(By.XPath("//input[@name='certificationName']"));
        private IWebElement CertifiedFromTextbox => driver.FindElement(By.XPath("//input[@name='certificationFrom']"));
        private IWebElement Year                 => driver.FindElement(By.XPath("//select[@name='certificationYear']"));
        private IWebElement AddButton            => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement successMessage       => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement UpdateNewButton      => driver.FindElement(By.XPath("//input[@value='Update']"));
        private Func<string, IWebElement> newCertificate = Certificate => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Certificate}']"));
        private Func<string, IWebElement> newCertifiedFrom = CertifiedFrom => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedFrom}']"));
        private Func<string, IWebElement> newYear = Year => driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Year}']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//input[@value='Cancel']"));


        public void Delete_All_Records()
        {
            try
            {
                Wait.WaitToBeClickable("XPath", "//div[@data-tab='fourth']//i[@class='remove icon']", 8);
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

        public void Add_Certification(CertificationData certificationData)
        {     
            //Click the add button
            AddNewButton.Click();
            //Enter the Certificate that needs to be added
            CertificateTextbox.SendKeys(certificationData.Certificate); 
            //Enter the certifiedFrom
            CertifiedFromTextbox.SendKeys(certificationData.CertifiedFrom);  
            //Choose the year
            SelectElement chooseYear = new SelectElement(Year);
            chooseYear.SelectByValue(certificationData.Year);
            //Click Add button
            AddButton.Click();
        }

        public string getMessage()
        {
            Wait.WaitToExist("XPath", "//div[@class='ns-box-inner']", 4);
            //Get the text message after adding certification
            return successMessage.Text;
        }

        public string getCertificate(string Certificate)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Certificate}']", 4);
            return newCertificate(Certificate).Text;
        }

        public string getCertifiedFrom(string CertifiedFrom)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Certifications']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedFrom}']", 4);
            return newCertifiedFrom(CertifiedFrom).Text;
        }

        public string getYear(string Year)
        {
            Thread.Sleep(4000);
            //Wait.WaitToExist("XPath", "//div[@class='four wide column' and h3='Certifications']/following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Year}']", 4);
            return newYear(Year).Text;
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

        public void Update_Certification(CertificationData existingCertificationData, CertificationData newCertificationData)
        {
            Thread.Sleep(4000);
            string xpath = $@"//div[@data-tab='fourth']//tr[td[1]='{existingCertificationData.Certificate}' " +
                                       $"and td[2]='{existingCertificationData.CertifiedFrom}' and td[3]='{existingCertificationData.Year}']/td[last()]/span[1]";
            IWebElement UpdateButton = driver.FindElement(By.XPath(xpath));
            //Click the update button
            UpdateButton.Click();
            //Clear and enter the certificate that needs to be updated
            CertificateTextbox.Clear();
            CertificateTextbox.SendKeys(newCertificationData.Certificate);
            //Clear and enter the certifiedFrom
            CertifiedFromTextbox.Clear();
            CertifiedFromTextbox.SendKeys(newCertificationData.CertifiedFrom);
            //Choose the year
            SelectElement chooseYear = new SelectElement(Year);
            chooseYear.SelectByValue(newCertificationData.Year);
            //Click the update button
            UpdateNewButton.Click();
        }

        public void Delete_Certification(CertificationData certificationData)
        {
            Thread.Sleep(4000);
            string xpath = $@"//div[@data-tab='fourth']//tr[td[1]='{certificationData.Certificate}' " +
                                 $"and td[2]='{certificationData.CertifiedFrom}' and td[3]='{certificationData.Year}']/td[last()]/span[2]";
            IWebElement DeleteButton = driver.FindElement(By.XPath(xpath));
            //Click the delete button that needs to be deleted
            DeleteButton.Click();
        }

        public string getDeletedCertification(CertificationData certificationData)
        {
            try
            {
                string xpath = $@"//div[@data-tab='fourth']//tr[td[1]='{certificationData.Certificate}' " +
                                 $"and td[2]='{certificationData.CertifiedFrom}' and td[3]='{certificationData.Year}']";
                IWebElement DeletedCertification = driver.FindElement(By.XPath(xpath));
                return DeletedCertification.Text;
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
    }
}
