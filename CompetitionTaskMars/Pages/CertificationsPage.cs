using CompetitionTaskMars.Data;
using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class CertificationsPage : CommonDriver
    {
        IReadOnlyCollection<IWebElement> deleteButtons => driver.FindElements(By.XPath("//div[@data-tab='fourth']//i[@class='remove icon']"));
        private IWebElement AddNewButton         => driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']//div"));
        private IWebElement CertificateTextbox   => driver.FindElement(By.XPath("//input[@name='certificationName']"));
        private IWebElement CertifiedFromTextbox => driver.FindElement(By.XPath("//input[@name='certificationFrom']"));
        private IWebElement Year                 => driver.FindElement(By.XPath("//select[@name='certificationYear']"));
        private IWebElement AddButton            => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement UpdateNewButton      => driver.FindElement(By.XPath("//input[@value='Update']"));

        public void Delete_All_Records()
        {
            Thread.Sleep(4000);
            //Delete all records in the list
            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();
            }

        }

        public void Add_Certification(CertificationData certificationData)
        {            
            AddNewButton.Click();
            CertificateTextbox.SendKeys(certificationData.Certificate);            
            CertifiedFromTextbox.SendKeys(certificationData.CertifiedFrom);            
            SelectElement chooseYear = new SelectElement(Year);
            chooseYear.SelectByValue(certificationData.Year);
            AddButton.Click();
        }

        public string getMessage()
        {
            Thread.Sleep(4000);
            IWebElement successMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return successMessage.Text;
        }

        public string getCertificate(string Certificate)
        {
            Thread.Sleep(4000);
            IWebElement newCertificate = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Certificate}']"));
            return newCertificate.Text;
        }

        public string getCertifiedFrom(string CertifiedFrom)
        {
            Thread.Sleep(4000);
            IWebElement newCertifiedFrom = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{CertifiedFrom}']"));
            return newCertifiedFrom.Text;
        }

        public string getYear(string Year)
        {
            Thread.Sleep(4000);
            IWebElement newYear = driver.FindElement(By.XPath($"//div[@class='four wide column' and h3='Certification']//following-sibling::div[@class='twelve wide column scrollTable']//td[text()='{Year}']"));
            return newYear.Text;
        }

        public void Update_Certification(CertificationData existingCertificationData, CertificationData newCertificationData)
        {
            Thread.Sleep(4000);
            IWebElement UpdateButton = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//tr[td[1]='{existingCertificationData.Certificate}' " +
                                       $"and td[2]='{existingCertificationData.CertifiedFrom}' and td[3]='{existingCertificationData.Year}']/td[last()]/span[1]"));
            UpdateButton.Click();
            CertificateTextbox.Clear();
            CertificateTextbox.SendKeys(newCertificationData.Certificate);
            CertifiedFromTextbox.Clear();
            CertifiedFromTextbox.SendKeys(newCertificationData.CertifiedFrom);
            SelectElement chooseYear = new SelectElement(Year);
            chooseYear.SelectByValue(newCertificationData.Year);
            UpdateNewButton.Click();
        }

        public void Delete_Certification(CertificationData certificationData)
        {
            Thread.Sleep(4000);
            IWebElement DeleteButton = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//tr[td[1]='{certificationData.Certificate}' " +
                                 $"and td[2]='{certificationData.CertifiedFrom}' and td[3]='{certificationData.Year}']/td[last()]/span[2]"));
            DeleteButton.Click();
        }

        public string getDeletedCertification(CertificationData certificationData)
        {
            try
            {
                IWebElement DeletedCertification = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//tr[td[1]='{certificationData.Certificate}' " +
                                 $"and td[2]='{certificationData.CertifiedFrom}' and td[3]='{certificationData.Year}']"));
                return DeletedCertification.Text;
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
    }
}
