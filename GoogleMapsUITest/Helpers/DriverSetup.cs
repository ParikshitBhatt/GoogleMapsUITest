using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace GoogleMapsUITest.Helpers
{
    public class DriverSetup
    {
        public static IWebDriver driver;

        // Enum for browser options
        private enum BrowserType { Chrome, Firefox, Edge }

        // Get the driver
        public static void Intilize()
        {
            try
            {
                if (Configuration.ExecutionEnvironment.ToLower().Equals("local"))
                {
                    driver = CreateLocalDriver();
                }
                else
                {
                    driver = new RemoteWebDriver(Configuration.ServerURL, Configuration.GetBrowserStackCapabilities());
                }
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = Configuration.DefaultTimeoutSeconds;
            }
            catch (Exception ex)
            {
                throw new WebDriverException($"Error initializing driver: {ex.Message}");
            }
        }

        // Get local instance of the driver
        private static IWebDriver CreateLocalDriver()
        {
            var browser = GetBrowserType(Configuration.BrowserName);

            switch (browser)
            {
                case BrowserType.Edge:
                    return new EdgeDriver();
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Chrome:
                default:
                    ChromeOptions options = new ChromeOptions();
                    //options.AddArgument("--incognito");
                    return new ChromeDriver(options);
            }
        }

        // Get the browser type
        private static BrowserType GetBrowserType(string browserName)
        {
            return Enum.Parse<BrowserType>(browserName, ignoreCase: true);
        }

        // Dispose the driver
        public static void DisposeDriver()
        {
            // Defensive check in case of errors
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
