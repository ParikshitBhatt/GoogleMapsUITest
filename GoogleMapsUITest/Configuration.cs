using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace GoogleMapsUITest
{
    internal static class Configuration
    {
        // BrowserStack Credentials (Ensure these are set in environment variables)
        private static string? BrowserStackUsername = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
        private static string? BrowserStackAccessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

        // Core Settings
        internal static string BaseURL = "https://www.google.com/maps";

        // Allow configuration via environment or config file 
        internal static string ExecutionEnvironment = GetConfigValue("EXECUTION_ENVIRONMENT", "Local"); // Local or BrowserStack
        internal static string BrowserName = GetConfigValue("BROWSER_NAME", "Chrome");
        internal static TimeSpan DefaultTimeoutSeconds = TimeSpan.FromSeconds(5);
        internal static Uri ServerURL = new Uri(GetServerURL());

        // Paths (Consolidated)
        private static string ProjectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        internal static string ReportPath = Path.Combine(ProjectPath, "TestReports", "ExtentReport.html");
        internal static string ScreenshotsDir = Path.Combine(ProjectPath, "TestReports", "Screenshots");

        // Configuration Helper
        private static string GetConfigValue(string key, string defaultValue = "")
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }

        // Check for BrowserStack Credentials and return Server URL
        private static string GetServerURL()
        {
            if (ExecutionEnvironment.ToLower().Equals("browserstack"))
            {
                if (string.IsNullOrEmpty(BrowserStackUsername) || string.IsNullOrEmpty(BrowserStackAccessKey))
                {
                    throw new Exception("BrowserStack credentials not found. Ensure BROWSERSTACK_USERNAME and BROWSERSTACK_ACCESS_KEY are set in environment variables.");
                }
            }
            return $"https://{BrowserStackUsername}:{BrowserStackAccessKey}@hub-cloud.browserstack.com:443/wd/hub";
        }

        // Conditional BrowserStack Capabilities
        internal static DriverOptions GetBrowserStackCapabilities()
        {
            Dictionary<string, string> bstackOptions = new Dictionary<string, string>();

            bstackOptions.Add("os", "Windows");
            bstackOptions.Add("osVersion", "11");
            bstackOptions.Add("browserVersion", "latest");
            bstackOptions.Add("consoleLogs", "info");
            bstackOptions.Add("projectName", "GoogleMapsUITest");
            bstackOptions.Add("buildName", "AddressSearchTests");

            DriverOptions? caps = null;

            switch (BrowserName)
            {
                case "Edge":
                    caps = new EdgeOptions();
                    caps.AddAdditionalOption("bstack:options", bstackOptions);
                    break;
                case "Firefox":
                    caps = new FirefoxOptions();
                    caps.AddAdditionalOption("bsatck:options", bstackOptions);
                    break;
                default:
                    caps = new ChromeOptions();
                    caps.AddAdditionalOption("bstack:options", bstackOptions);
                    break;
            }

            return caps;
        }

    }
}