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
    public class FlightStatusSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait,ShorterbDriverWait;

        public FlightStatusSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            ShorterbDriverWait=new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
        }


        [Given(@"User Navigate to Flight Status Service")]
        public void GivenUserNavigatetoFlightStatusService()
        {
           EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.FlightStatusServicePage);
        }

        [Then(@"User will see yesterday, today, and tomorrow")]
        public void ThenUserwillseeyesterdaytodayandtomorrow()
        {
            

           Thread.Sleep(5400);
           // DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.FlightStatusServicePage);
            EnvironmentHelper.Scroll(_webDriver);
            TimeZoneInfo uaeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dubai");

            // Convert the current time to UAE timezone
            DateTime uaeDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, uaeTimeZone);

            string expectedTodayName = uaeDateTime.ToString("dddd", new System.Globalization.CultureInfo("ar-AE"));
            string yesterdayName = uaeDateTime.AddDays(-1).ToString("dddd", new System.Globalization.CultureInfo("ar-AE"));
            string tomorrowName = uaeDateTime.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("ar-AE"));
           
            WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.dayNameInArabic, ElementState.VISIBLE);
            Assert.That(expectedTodayName,Is.EqualTo(_webDriver.FindElement(FlightStatusPage.dayNameInArabic).Text.ToString()));
             WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.FlighCard, ElementState.VISIBLE);

            IWebElement YesterdayFilterButtonelement = _webDriver.FindElement(FlightStatusPage.YesterdayFilterButton);
            IWebElement tomorrowFilterButtonelement = _webDriver.FindElement(FlightStatusPage.tomorrowFilterButton);

            Actions actions = new Actions(_webDriver);
            
                 actions.MoveToElement(YesterdayFilterButtonelement, YesterdayFilterButtonelement.Size.Width / 2, YesterdayFilterButtonelement.Size.Height / 2).Perform();
                 actions.Click().Perform();    
                  
             string classes = YesterdayFilterButtonelement.GetAttribute("class");
           
             if(classes.Contains("lightGrayColor")){
              ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].style.padding = '0px';", YesterdayFilterButtonelement);
              Thread.Sleep(2000);
              YesterdayFilterButtonelement.Click();
             }
                                          
            WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.YesterdayFilterButton, ElementState.VISIBLE);
            Thread.Sleep(1500);
            Assert.That(_webDriver.FindElement(FlightStatusPage.dayNameInArabic).Text.ToString(),Is.EqualTo(yesterdayName));
            WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.FlighCard, ElementState.VISIBLE);


            _webDriver.FindElement(FlightStatusPage.tomorrowFilterButton).Click();


            actions.MoveToElement(tomorrowFilterButtonelement).Click().Perform();

            Thread.Sleep(1500);
            WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.tomorrowFilterButton, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElement(FlightStatusPage.dayNameInArabic).Text.ToString(),Is.EqualTo(tomorrowName));
             WaitUtils.elementState(_webDriver,_webDriverWait, FlightStatusPage.FlighCard, ElementState.VISIBLE);
 
             var CompanyName= _webDriver.FindElement(FlightStatusPage.FirstFlightCompanyName).Text.ToString();
             var flightCode= _webDriver.FindElement(FlightStatusPage.FirstFlightCode).Text.ToString();
            
             IWebElement searchInput=  _webDriver.FindElement(FlightStatusPage.SearchInput);
             EnvironmentHelper.EnterTextLetterByLetter(searchInput,CompanyName);  
             Thread.Sleep(500);
             var shownFlightsFromDifferentCompany = _webDriver.FindElements(By.XPath("//p[contains(@class, 'mainColor') and contains(@class, 'text-uppercase') and not(contains(text(), '"+CompanyName+"'))]")).Count; 
             Assert.AreEqual(shownFlightsFromDifferentCompany,0);
             searchInput.Clear();
             searchInput.SendKeys(Keys.Space);

       }

    }
}
