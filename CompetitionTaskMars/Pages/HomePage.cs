using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class HomePage : CommonDriver
    {
        private IWebElement EducationTab => driver.FindElement(By.XPath("//a[text()='Education']"));

        public void GoToEducationPage()
        {
            Thread.Sleep(6000);
            
            EducationTab.Click();
        }
    }
}
