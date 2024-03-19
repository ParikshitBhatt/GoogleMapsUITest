using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace GoogleMapsUITest.Helpers
{
    internal class ExtentReporter
    {
        private static ExtentReports _extent;
        public static ExtentTest _test;

        // Initialize ExtentReports
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentSparkReporter(Configuration.ReportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);

        }

        // Create a new test within the report
        public static void CreateTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        // Log test step with screenshot capture
        private static void LogWithScreenshot(IWebDriver driver, Status status, string message, string fileName)
        {
            fileName = fileName.Split(" ")[0];
            string screenshotPath = HelperFunctions.CaptureScreenshot(driver, fileName + ".png");
            _test.Log(status, message).AddScreenCaptureFromPath(screenshotPath);
        }

        // Log test steps: Pass
        public static void LogPass(IWebDriver driver, string message, string fileName)
        {
            LogWithScreenshot(driver, Status.Pass, message, fileName);
            HelperFunctions.BrowserStackMarkStatus("passed", message, driver);
        }

        // Log test steps: Fail
        public static void LogFail(IWebDriver driver, string message, string fileName)
        {
            LogWithScreenshot(driver, Status.Fail, message, fileName);
            HelperFunctions.BrowserStackMarkStatus("failed", message, driver);
        }

        // Log test steps: Info
        public static void LogInfo(string message)
        {
            _test.Log(Status.Info, message);
        }

        // Flush the report
        public static void FlushReport()
        {
            _extent.Flush();
        }
    }
}
