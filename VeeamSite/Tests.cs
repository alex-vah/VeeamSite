using System;
using NUnit.Framework;
using System.Threading;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace VeeamSite
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void JobCount()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();//Connecting to the configuration file
            
            IWebDriver driver = new ChromeDriver(configuration["driverPath"]);  
            driver.Navigate().GoToUrl("https://cz.careers.veeam.com/vacancies");//Going to the website
            driver.Manage().Window.Maximize();//maximizing the window
            IWebElement selectDepartment = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/div[1]/div/div[2]/div/div/button"));
            selectDepartment.Click();//finding and clicking the departments dropdown
            Thread.Sleep(100);//just to see what's going on
            IWebElement department = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/div[1]/div/div[2]/div/div/div/a[3]"));
            department.Click();//finding and clicking on Research & development
            Thread.Sleep(100);//just to see what's going on
            IWebElement languages = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/div[1]/div/div[3]/div/div/button"));
            languages.Click();//finding and clicking the languages dropdown
            Thread.Sleep(100);//just to see what's going on
            IWebElement english = driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/div[1]/div/div[3]/div/div/div/div[1]/input"));
            english.Click();//finding and checking English checkbox
            Thread.Sleep(100);//just to see what's going on
            languages.Click();//closing the dropdown 
            var elements = driver.FindElements(By.XPath("/html/body/div/div/div[1]/div/div/div[2]/div/a"));
            Console.WriteLine($"Number of jobs found: {elements.Count}");
            Console.WriteLine($"Expected number of jobs: {configuration["expectedJobCount"]}");
            Assert.AreEqual(elements.Count, Convert.ToInt32(configuration["expectedJobCount"]));//comparing result and the expected value
            Thread.Sleep(100);//just to see what's going on
            driver.Close();
        }
    }
}