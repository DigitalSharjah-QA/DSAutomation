using TechTalk.SpecFlow;
using SD.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SD.Pages;
using SD.Helpers.Enums;
using System;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;
using SD.Datasources.Users;
using System.Text.RegularExpressions;
using SD.Datasources.Attachments;
using OpenQA.Selenium.Interactions;
using SD.Steps.Helpers;
using FluentAssertions;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.Steps.Eservice.PROD
{
    [Binding]
    public class WeatherServiceSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait,ShorterbDriverWait;


        public WeatherServiceSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            ShorterbDriverWait=new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
        }

       
[Given(@"User click on General Category")]
public void GivenUserclickonGeneralCategory()
{
	        WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.GeneralCategory, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, HomePage.GeneralCategory);
            _webDriver.FindElement(HomePage.GeneralCategory).Click();
}

[Then(@"User in Weather Service Page")]
public void ThenUserinWeatherServicePage()
{
	        DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.WeatherServicePage);
            Thread.Sleep(2000);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, WeatherPage.TodayWeatherWidget, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, WeatherPage.SixDaysWidgets, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, WeatherPage.SixDates, ElementState.VISIBLE);
            string actualValue =  _webDriver.FindElement(WeatherPage.CurrentTempValue).Text.ToString();
            // Extract the numeric part (integer) from the actual value
            string numericPart = actualValue.Split('°')[0].Trim();

            // Attempt to convert the numeric part to an integer
            bool conversionSuccess = int.TryParse(numericPart, out int actualIntValue);

            // Assert that the conversion was successful and the integer is not zero
            Assert.IsTrue(conversionSuccess, "Failed to convert to integer.");
            // Assert.Greater(actualIntValue, 0, "The integer value is not greater than 0.");
}




    }
}
