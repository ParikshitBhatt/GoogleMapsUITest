using GoogleMapsUITest.Pages;
using GoogleMapsUITest.Helpers;
using GoogleMapsUITest.TestResources;

namespace GoogleMapsUITest.Tests
{

    [TestFixture]
    internal class AddressSearchTests : DriverSetup
    {
        private GoogleMapsHomePage _mapPage;

        [OneTimeSetUp]
        // Setup the reporting
        public void SetupReporting()
        {
            ExtentReporter.InitializeReport();
        }

        [SetUp]
        // Setup the driver and create a new test in the report
        public void BeforeEachTest()
        {
            // Initialize browser based on Configuration if available
            Intilize();

            ExtentReporter.CreateTest(TestContext.CurrentContext.Test.Name);
            _mapPage = new GoogleMapsHomePage(driver);
        }

        // Positive Test Scenarios

        //Search for Specific Addresses using TestCase attribute for parameterized testing
        [TestCase(TestData.WoogaHQ)]
        [TestCase(TestData.HCLTechHQ)]
        [TestCase(TestData.PlaytikaHQ)]
        [TestCase(TestData.GoogleHQ)]
        [TestCase(TestData.MetaHQ)]
        public void TestSearchForSpecificAddress(string address)
        {
            try
            {
                _mapPage.SearchForAddress(address);
                ExtentReporter.LogInfo($"{address}: Search for specific address");
                Assert.IsTrue(_mapPage.IsLocationDisplayed(address: address));
                ExtentReporter.LogPass(driver, $"{address}: Test search for specific address successful", address);
            }
            catch (Exception e)
            {
                ExtentReporter.LogFail(driver, e.Message, address);
                throw;
            }
        }


        //Search for Points of Interest using TestCaseSource attribute for parameterized testing
        [TestCaseSource(typeof(TestData), nameof(TestData.PointsOfInterest))]
        public void TestSearchForPointOfInterest(string landmark)
        {
            try
            {
                _mapPage.SearchForAddress(landmark);
                ExtentReporter.LogInfo($"{landmark}: Search for point of interest");
                Assert.IsTrue(_mapPage.IsLocationDisplayed(address: landmark));
                ExtentReporter.LogPass(driver, $"{landmark}: Test search for point of interest successful", landmark);
            }
            catch (Exception e)
            {
                ExtentReporter.LogFail(driver, e.Message, landmark);
                throw;
            }
        }

        //Negative Test Scenarios
        //Search for Invalid Addresses
        [TestCase("kf214345dsajfk2421231lsdjf324")] // Clearly non-existent address
        public void TestSearchForInvalidAddress(string address)
        {
            try
            {
                _mapPage.SearchForAddress(address);
                ExtentReporter.LogInfo($"{address}: Search for invalid address");
                Assert.IsTrue(_mapPage.IsErrorMessageDisplayed());
                ExtentReporter.LogPass(driver, $"{address}: Test search for invalid address successful", address);
            }
            catch (Exception e)
            {
                ExtentReporter.LogFail(driver, e.Message, address);
                throw;
            }
        }

        //Search for Very Long Addresses
        [TestCase(TestData.VeryLongAddress)]
        public void TestSearchWithVeryLongAddress(string address)
        {
            try
            {
                _mapPage.SearchForAddress(address);
                ExtentReporter.LogInfo($"{address}: Search with very long address");
                Assert.IsTrue(_mapPage.IsErrorMessageDisplayed());
                ExtentReporter.LogPass(driver, $"{address}: Test search with very long address successful", address);
            }
            catch (Exception e)
            {
                ExtentReporter.LogFail(driver, e.Message, address);
                throw;
            }
        }

        //Edge Cases
        //Search for Address with Special Characters
        [TestCase(TestData.QueryWithSpecialChars)]
        public void TestSearchWithSpecialCharacters(string query)
        {
            try
            {
                _mapPage.SearchForAddress("Berlin");
                ExtentReporter.LogInfo($"Searching for Berlin");
                _mapPage.SearchForAddress(query);
                ExtentReporter.LogInfo($"{query}: Search with special characters");
                Assert.IsTrue(_mapPage.IsLocationDisplayed(query: query));
                ExtentReporter.LogPass(driver, $"{query}: Test search with special characters successful", query);
            }
            catch (Exception e)
            {
                ExtentReporter.LogFail(driver, e.Message, query);
                throw;
            }
        }

        [TearDown]
        // Close the driver and end the test in the report
        public void Cleanup()
        {
            DisposeDriver();
        }

        [OneTimeTearDown]
        // Cleanup the reporting
        public void CleanupReporting()
        {
            ExtentReporter.FlushReport();
        }
    }
}
