using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using System;

namespace FinalProject.Driver
{
    class CustomDriver
    {
        public static IWebDriver GetChromeDriver()
        {
            return GetDriver(Browsers.Chrome);
        }

        public static IWebDriver GetFirefoxDriver()
        {
            return GetDriver(Browsers.Firefox);
        }

        public static IWebDriver GetIncognitoChrome()
        {
            return GetDriver(Browsers.IncognitoChrome);
        }

        private static IWebDriver GetDriver(Browsers browserName)
        {
            IWebDriver driver = null;

            switch (browserName)
            {
                case Browsers.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case Browsers.Opera:
                    driver = new OperaDriver();
                    break;
                case Browsers.IncognitoChrome:
                    driver = GetChromeWithOptions();
                    break;
            }
            //Laukiame 10s kol kiekviena elementas užsikrauna
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            return driver;
        }
        /// <summary>
        /// metodas incognito puslapiui paleisti
        /// </summary>
        /// <returns>gražina incognito web draiverį</returns>
        private static IWebDriver GetChromeWithOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            options.AddArgument("start-maximized");
            // options.AddArguments("start-maximized", "incognito");
            return new ChromeDriver(options);
        }
    }
}