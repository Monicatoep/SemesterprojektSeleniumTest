using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterprojektSeleniumTest
{
    [TestClass]
    public class PhotoDeleteTest
    {
        static string URL = "http://seasonalstory-csgyagc7gzfkdgf0.northeurope-01.azurewebsites.net";

        private static IWebDriver _driver = new ChromeDriver();

        [TestMethod]

        public void DeleteTest()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(URL);

            // Wait for the cards to be visible
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".card-body")));
            var seasons = _driver.FindElements(By.CssSelector(".card-body"));
            IWebElement winterCard = seasons.FirstOrDefault(season => season.FindElement(By.TagName("h5")).Text == "Vinter");
            winterCard.FindElement(By.Id("image")).Click();

            WebDriverWait _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Find og vent på, at alle delete-knapper er synlige
            IReadOnlyCollection<IWebElement> deleteButtons = _wait.Until(d =>
            {
                var elements = d.FindElements(By.CssSelector("#showDeletePageButton"));
                return elements.Any(e => e.Displayed) ? elements : null;
            });

            int firstCount = deleteButtons.Count();

            // Get the last delete button
            if (deleteButtons.Count > 0)
            {
                IWebElement lastDeleteButton = null;
                foreach (var button in deleteButtons)
                {
                    lastDeleteButton = button; // Overwrite until the last button is reached
                }

                // Click the last delete button
                lastDeleteButton?.Click();
            }

            IWebElement deleteButton = _driver.FindElement(By.Id("deleteButton"));
            deleteButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".card-body")));
            var seasons2 = _driver.FindElements(By.CssSelector(".card-body"));
            IWebElement winterCard2 = seasons2.FirstOrDefault(season => season.FindElement(By.TagName("h5")).Text == "Vinter");
            winterCard2.FindElement(By.Id("image")).Click();

            IReadOnlyCollection<IWebElement> deleteButtons2 = _wait.Until(d =>
            {
                var elements = d.FindElements(By.CssSelector("#showDeletePageButton"));
                return elements.Any(e => e.Displayed) ? elements : null;
            });

            int secondCount = deleteButtons2.Count();
            Assert.AreEqual(firstCount - 1, secondCount);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

    }
}
