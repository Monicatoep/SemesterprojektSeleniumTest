using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace SemesterprojektSeleniumTest
{
    [TestClass]
    public class PhotoAddTest
    {
        static string URL = "http://seasonalstory-csgyagc7gzfkdgf0.northeurope-01.azurewebsites.net";

        private static IWebDriver _driver = new ChromeDriver();

        [TestMethod]

        public void AddTest()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(URL);
            // Wait for the cards to be visible
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".card-body")));

            var seasons = _driver.FindElements(By.CssSelector(".card-body"));
            IWebElement winterCard = seasons.FirstOrDefault(season => season.FindElement(By.TagName("h5")).Text == "Vinter");
            winterCard.FindElement(By.Id("image")).Click();

            IWebElement addButton = _driver.FindElement(By.Id("addButton"));
            addButton.Click();

            IWebElement radioButton = _driver.FindElement(By.Id("temp-1"));
            radioButton.Click();

            //string projectDirectory =  "SemesterprojektSeleniumTest\SemesterprojektSeleniumTest\TestPhotos\TestBillede.jpg"
            //string filePath = Path.Combine(projectDirectory, @"TestPhotos\TestBillede.jpg");
            //filePath = Path.GetFullPath(filePath); // Ensure it's an absolute path
            // Find roden af projektet (to niveauer op fra bin/Debug)
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            // Find filstien i en mappe inkluderet i din solution (TestPhotos i projektroden)
            string filePath = Path.Combine(projectDirectory, "TestPhotos", "TestBillede.jpg");
            filePath = Path.GetFullPath(filePath); // Konverter til absolut sti

            IWebElement fileInput = _driver.FindElement(By.Id("formFile"));
            
            fileInput.SendKeys(filePath);

            IWebElement uploadButton = _driver.FindElement(By.Id("uploadButton"));
            uploadButton.Click();

            //WebDriverWait _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //IWebElement addMessage = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("outputMessage")));
            //string output = addMessage.Text;
            //Assert.AreEqual<string>("Response: 201 Created", output);

            WebDriverWait _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement addMessage = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popUpMessage")));
            string output = addMessage.Text;
           
            Assert.AreEqual<string>("Succes: Billedet er uploadet\r\nOK", output);

        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }
    }
}
