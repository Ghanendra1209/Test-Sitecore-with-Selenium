//Inside SeleniumTest.cs

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;

using System;

using System.Collections.ObjectModel;

using System.IO;

namespace SeleniumCsharp

{

    public class Tests

    {

        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start=maximized");
            options.AddArguments("--lang=es");

            driver = new ChromeDriver(path + @"\drivers\", options);

            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }

        [Test]

        public void verifyLogin()

        {

            driver.Navigate().GoToUrl("https://<your sitecore instance url>/sitecore");

            var uName = driver.FindElement(By.Id("Username"));
            uName.SendKeys("<yourusername>");
            var password = driver.FindElement(By.Id("Password"));
            password.SendKeys("<yourpassword>");

            var loginButton = driver.FindElement(By.Name("button"));
            loginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Assert.IsTrue(driver.FindElement(By.ClassName("sc-accountInformation"))!=null);

        }

        [Test]

        public void returnToLoginOnInvalidCreds()

        {

            driver.Navigate().GoToUrl("https://<your sitecore instance url>/sitecore");

            var uName = driver.FindElement(By.Id("Username"));
            uName.SendKeys("<yourusername>");
            var password = driver.FindElement(By.Id("Password"));
            password.SendKeys("<incorrectpassword>");

            var loginButton = driver.FindElement(By.Name("button"));
            loginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Assert.IsTrue(driver.FindElement(By.Id("Username")) != null);

        }



        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }

}
