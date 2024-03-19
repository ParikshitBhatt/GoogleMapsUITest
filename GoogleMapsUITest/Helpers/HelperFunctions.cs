using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace GoogleMapsUITest.Helpers
{
    internal class HelperFunctions
    {
        // Wait for element to be exist
        public static IWebElement WaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 45)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(ExpectedConditions.ElementExists(locator));
            }
            catch (NoSuchElementException)
            {
                throw new TimeoutException($"Element with locator '{locator}' was not found within {timeoutInSeconds} seconds.");
            }
        }

        // Capture screenshot
        public static string CaptureScreenshot(IWebDriver driver, string fileName)
        {
            if(!Directory.Exists(Configuration.ScreenshotsDir))
                Directory.CreateDirectory(Configuration.ScreenshotsDir); // Create if it doesn't exist
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string filePath = Path.Combine(Configuration.ScreenshotsDir, fileName);
            screenshot.SaveAsFile(filePath);
            return filePath;
        }

        // Check if element is visible
        public static bool IsElementVisible(IWebDriver driver, By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // BrowserStack Mark Test Status
        public static void BrowserStackMarkStatus(string status, string reason, IWebDriver driver)
        {
            if (Configuration.ExecutionEnvironment.ToLower().Equals("browserstack"))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"" + status + "\", \"reason\": \"" + reason + "\"}}");
            }
        }
    }
}
