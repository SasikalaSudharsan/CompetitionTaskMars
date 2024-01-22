using CompetitionTaskMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionTaskMars.Pages
{
    public class LoginPage : CommonDriver
    {
        private IWebElement SignInButton => driver.FindElement(By.XPath("//div[@id='home']/div/div/div/div/a"));
        private IWebElement UsernameTextbox => driver.FindElement(By.XPath("//input[@name='email']"));
        private IWebElement PasswordTextbox => driver.FindElement(By.XPath("//input[@name='password']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//button[text()='Login']"));

        public void LoginActions()
        {
            //Click SignIn button
            SignInButton.Click();
            //Wait.WaitToExist("XPath", "//input[@name='email']", 4);
            //Enter username
            UsernameTextbox.SendKeys("sasi.ei34@gmail.com");
            //Enter password
            PasswordTextbox.SendKeys("Selenium@2");
            //Click login button
            LoginButton.Click();
        }
    }
}
