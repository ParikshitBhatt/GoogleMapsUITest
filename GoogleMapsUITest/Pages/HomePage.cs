using GoogleMapsUITest.Helpers;
using OpenQA.Selenium;

namespace GoogleMapsUITest.Pages
{
    internal class GoogleMapsHomePage
    {
        private readonly IWebDriver _driver;
        private const int timeoutInSeconds = 5;

        // Locators
        private By _searchBox = By.Id("searchboxinput");
        private By _searchButton = By.Id("searchbox-searchbutton");
        private By _directionsButton = By.XPath("//button[contains(@aria-label, 'Directions')]");
        private By _searchResultsContainer = By.XPath("//*[@role='main']");
        private By _searchResultsList = By.XPath("//*[@role='feed']/div/div/a");
        private By _chooseStartingPoint = By.Id("directions-searchbox-0");
        private By _directionsResults = By.Id("section-directions-trip-0");
        private By _errorMessage = By.XPath("//*[contains(text(), \"Google Maps can't find\")]");

        public GoogleMapsHomePage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(Configuration.BaseURL);
        }

        // Search for an address
        public void SearchForAddress(string address)
        {
            _driver.FindElement(_searchBox).Clear();
            _driver.FindElement(_searchBox).SendKeys(address);
            _driver.FindElement(_searchButton).Click();
            HelperFunctions.WaitForElement(_driver, _searchResultsContainer, timeoutInSeconds); // Ensure result loads
        }

        // Directions with optional starting point
        public void ClickDirections(string? startingPoint = null)
        {
            _driver.FindElement(_directionsButton).Click();
            if (startingPoint != null)
            {
                _driver.FindElement(_chooseStartingPoint).SendKeys(startingPoint + Keys.Enter);
                HelperFunctions.WaitForElement(_driver, _directionsResults, timeoutInSeconds).Click();
            }
        }

        //Robust location verification (Handles both addresses and search queries)
        public bool IsLocationDisplayed(string? address = null, string? query = null)
        {
            if (address != null)
            {
                return IsAddressResultDisplayed(address);
            }
            else if (query != null)
            {
                return IsSearchResultDisplayed(query);
            }
            else
            {
                throw new ArgumentException("Either 'address' or 'query' must be provided");
            }
        }

        //Search results extraction
        public List<string> GetSearchResults()
        {
            List<string> results = new List<string>();
            HelperFunctions.WaitForElement(_driver, _searchResultsList, timeoutInSeconds);
            var resultElements = _driver.FindElements(_searchResultsList);
            foreach (IWebElement resultElement in resultElements)
            {
                string resultElementText = resultElement.GetAttribute("aria-label");
                results.Add(resultElementText);
            }
            return results;
        }

        //Address result verification
        public bool IsAddressResultDisplayed(string address)
        {
            bool isDisplayed = false;
            try { isDisplayed = HelperFunctions.WaitForElement(_driver, _addressResult(address.Split(",")[0]), timeoutInSeconds).Displayed; }
            catch { var results = GetSearchResults(); isDisplayed = results.Any(result => result.Contains(address.Split(",")[0])); }
            return isDisplayed;
        }

        //Search result verification
        public bool IsSearchResultDisplayed(string query)
        {
            var results = GetSearchResults();
            return results.Any(result => result.Contains(query));
        }

        //Error message verification
        public bool IsErrorMessageDisplayed()
        {
            return HelperFunctions.IsElementVisible(_driver, _errorMessage);
        }

        //Address result locator
        private By _addressResult(string textValue)
        {
            string firstPart = textValue.Split(" ")[0];
            return By.XPath($"//h1[contains(text(), '{firstPart}')]");
        }
    }
}