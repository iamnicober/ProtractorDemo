using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protractor;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace ProtractorDemo
{
    [TestClass]
    public class TestProtractor
    {
        private IWebDriver driver;
        private NgWebDriver ngDriver;
        private WebDriverWait WaitTime;
        private string URL = "https://s3.ap-south-1.amazonaws.com/shiva1792.tk/DiscountCalculator/ShoppingDiscount.html";

        [TestInitialize]
        public void Start()
        {
            driver = new FirefoxDriver(@"C:\Users\Shiva\Downloads");
            driver.Manage().Window.Maximize();
            WaitTime = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            ngDriver = new NgWebDriver(driver);
        }

        [TestMethod]
        public void FirstTest()
        {
            driver.Navigate().GoToUrl(URL);
            ngDriver.WaitForAngular();           

            //Find product price text box using the ng-model 
            NgWebElement ProductPrice = ngDriver.FindElement(NgBy.Model("productPrice"));
            ProductPrice.SendKeys("799");

            //Find discount text box using the ng-model 
            NgWebElement DiscountOnProduct = ngDriver.FindElement(NgBy.Model("discountPercent"));
            DiscountOnProduct.SendKeys("10");

            //Find button using selenium locator XPath
            NgWebElement BtnPriceAfterDiscount = ngDriver.FindElement(By.XPath("//*[@id='f1']/fieldset[2]/input[1]"));
            BtnPriceAfterDiscount.Click();

            //Find discounted product text box using the ng-model 
            NgWebElement afterDiscountValue = ngDriver.FindElement(NgBy.Model("afterDiscount"));
            string value = afterDiscountValue.GetAttribute("value");

            //Assert for corect value
            Assert.AreEqual<string>("719.1", value);

            //Use if condition for custom checks
            if (value == "719.1")
            {
                //Do Nothing.
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestCleanup]
        public void Stop()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
