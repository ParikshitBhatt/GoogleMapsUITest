# Google Maps UI Automation Framework

This readme provides a comprehensive overview of the Google Maps UI Automation Framework, designed for testing Google Maps functionalities using Selenium WebDriver.

## Features

- **Cross-browser support:** Execute tests on multiple browsers (Chrome, Firefox, and Edge.) for broader test coverage.
- **Local/BrowserStack Execution:** Run tests locally on your machine or leverage BrowserStack for cloud-based testing across various browser and device combinations.
- **Integrated Screenshots:** Capture screenshots on test passes/failures and embed them within Extent Reports for visual reference and debugging.
- **Detailed Reporting:** Generate comprehensive reports using ExtentReports, including test status, logs, and screenshots.
- **Modular Design:** The framework utilizes Page Object Model (POM) for better code organization and maintainability.
- **Data-Driven Testing:** Allows for parameterization of test data (addresses, landmarks) for efficient test execution.
- **Easy Setup:** Designed for straightforward configuration and usage.

## Tech Stack

- **Programming Language:** C#
- **Testing Framework:** NUnit
- **CI pipeline:** Git Actions
- **UI Automation Tool:** Selenium WebDriver
- **Reporting Library:** ExtentReports

## Getting Started

### Prerequisites:

- Install Visual Studio.
- Download and install NuGet packages (Selenium WebDriver, NUnit, ExtentReports).
- Configure BrowserStack credentials (optional) for cloud-based testing.

### Project Structure:

The framework is organized into several folders:
- **Pages:** Contains Page Object Model classes representing Google Maps webpages.
- **Helpers:** Houses utility classes for common functionalities (driver setup, extent repoter and some helper functions).
- **TestResources:** Stores test data (addresses, landmarks) in separate files (TestData.cs).
- **Tests:** Contains test cases for Google Maps Address Search Feature.

### Configuration (Optional):

A Configuration class (Configuration.cs) can be used to manage test settings (like execution environment, browser, and important path) and BrowserStack credentials.

### Running Tests:

# Running Tests Locally

## Prerequisites:

Ensure you have the .Net on your local machine.

## Build Project:

In your IDE or terminal, build the project to compile the test code.

## Execution:

- **With IDE:** If your IDE (like Visual Studio) has a built-in test runner, use it to execute your tests directly.
- **With Command Line:** Use the `dotnet test` command in your terminal to run the tests.

# Running Tests in Git Actions

## Git Repository:

Your code should be in a Git repository.

## Git Actions Workflow:

Setup and Execute Git Actions workflow file (path, `.github/workflows/GoogleMapsUITests-Pipeline.yml`).
