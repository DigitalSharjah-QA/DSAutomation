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

namespace SD.Steps.Eservice.Stagging.General
{

    [Binding]
    public class LogoutSteps
    { 

        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        public string TokenValue="";
        public IJavaScriptExecutor js;

        public LogoutSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            js = (IJavaScriptExecutor)_webDriver;
        }


[When(@"User clicks on the Logout button")]
public void WhenUserclicksontheLogoutbutton()
{
	            WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.UserIcon, ElementState.VISIBLE);
                _webDriver.FindElement(HomePage.UserIcon).Click();
                WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.UserName, ElementState.VISIBLE);
                Assert.IsNotNull(HomePage.UserName);
                 
                TokenValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
                Assert.GreaterOrEqual(TokenValue.Length,230);
                WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.LogoutButton, ElementState.VISIBLE);
                _webDriver.FindElement(HomePage.LogoutButton).Click();
  

}

[Then(@"User should be logged out successfully")]
public void ThenUsershouldbeloggedoutsuccessfully()
{
                Thread.Sleep(999);
	            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.UaePassPage);
                TokenValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
                Assert.LessOrEqual(TokenValue.Length,210);
}



    }
}
