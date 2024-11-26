using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SemesterprojektSeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        static string DriverDirectory = "C:\\webDrivers\\chromedriver-win64";
        static string URL = "http://127.0.0.1:5500/SSFrontend/index.html";
      
        public IWebDriver driver = new ChromeDriver();
    
        [TestMethod]
        public void TestRightSeasonAndGoBack()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(URL);
            // Wait for the cards to be visible
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".card-body")));

            // Identify the card by season title and click on the corresponding image
            var seasons = driver.FindElements(By.CssSelector(".card-body"));
            IWebElement springCard = seasons.FirstOrDefault(season => season.FindElement(By.TagName("h5")).Text == "Forår");
            springCard.FindElement(By.Id("image")).Click();

            // Verify the season folder is shown with the correct details
            var seasonTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2"))).Text;
            Assert.AreEqual("Forår", seasonTitle, "The season title displayed is incorrect.");

            //Test that the return button works
            IWebElement backButton = driver.FindElement(By.Id("backButton"));
            backButton.Click();

            IWebElement h1 = driver.FindElement(By.TagName("h1"));
            Assert.AreEqual("Velkommen til Seasonal Stories!", h1.Text);
        }
    }
}

    

