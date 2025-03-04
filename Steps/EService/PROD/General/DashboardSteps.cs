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
    public class DashboardSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait, ShorterbDriverWait;


        public DashboardSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            ShorterbDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
        }






[When(@"User Navigate to Dashboard Page")]
public void WhenUserNavigatetoDashboardPage()
{
        	try{
            Thread.Sleep(3000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.HomePage);
            WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.DashboardLink, ElementState.VISIBLE);
	        _webDriver.FindElement(HomePage.DashboardLink).Click(); 
        	DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.Dashboard);
            }
            catch{
                EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.Dashboard);
            }
            Thread.Sleep(2000);
            Assert.AreEqual("إجمالي الطلبات",_webDriver.FindElement(Dashboard.TotalRequestLabel).Text.ToString());
            Assert.AreEqual("منجز",_webDriver.FindElement(Dashboard.DoneRequestLabel).Text.ToString());
            Assert.AreEqual("قيد التنفيذ",_webDriver.FindElement(Dashboard.PendingRequestLabel).Text.ToString());
            Assert.AreEqual("رفضت",_webDriver.FindElement(Dashboard.RejectedRequestLabel).Text.ToString());
}

[Then(@"User in Dashboard Page He Should see Count of his Requests Categorized")]
public void ThenUserinDashboardPageHeShouldseeCountifhisRequestCategorized()
{
            IWebElement TotalRequestCount = _webDriver.FindElement(Dashboard.TotalRequestCountValue);
            IWebElement DoneRequestCount = _webDriver.FindElement(Dashboard.DoneRequestCountValue);
            IWebElement PendingRequestCount = _webDriver.FindElement(Dashboard.PendingRequestCountValue);
            IWebElement RejectedRequestCount = _webDriver.FindElement(Dashboard.RejectedRequestCountValue);

            bool totalValueChanged = false;
            bool doneValueChanged = false;
            bool pendingValueChanged = false;
            bool rejectedValueChanged = false;
            int tries=0;

            // Keep checking the values of the elements until they all change from the initial value
            while (!totalValueChanged || !doneValueChanged || !pendingValueChanged || !rejectedValueChanged)
            {
                // Get the current values of all four elements
                int totalValue = int.Parse(TotalRequestCount.Text);
                int doneValue = int.Parse(DoneRequestCount.Text);
                int pendingValue = int.Parse(PendingRequestCount.Text);
                int rejectedValue = int.Parse(RejectedRequestCount.Text);

                // Check if each value has changed from the initial value
                if (totalValue != 0) totalValueChanged = true;
                if (doneValue != 0) doneValueChanged = true;
                if (pendingValue != 0) pendingValueChanged = true;
                if (rejectedValue != 0) rejectedValueChanged = true;

                // If all values have changed, exit the loop
                if ((totalValueChanged && doneValueChanged && pendingValueChanged && rejectedValueChanged) ||(tries>=100))
                {
                    break;
                }

                if (!totalValueChanged && !doneValueChanged && !pendingValueChanged && !rejectedValueChanged)
                {
                    Assert.IsTrue(false);
                }
                // Wait for a short period before checking again
                Thread.Sleep(1000); 
                tries++;
            }
                       Thread.Sleep(555);
                       var WeatherDegree = _webDriver.FindElement(Dashboard.WeatherDegreeValue).Text.ToString();
                       int weatherDegreeValue = int.Parse(WeatherDegree);
                       Assert.That(weatherDegreeValue, Is.GreaterThan(10));

 }




    }
}
