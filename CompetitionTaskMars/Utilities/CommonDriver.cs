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

        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://localhost:5000/Home");
        }

        public void Close()
        {
            driver.Quit();
        }
    }
}
