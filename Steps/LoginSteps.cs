using FluentAssertions;
using OpenQA.Selenium;
using SD.Pages;
using System;
using TechTalk.SpecFlow;
using SD.Helpers;
using SD.Datasources;
using System.Threading;
using SD.Datasources.Users;
using OpenQA.Selenium.Support.UI;
using SD.Helpers.Enums;
using SD.Steps.Helpers;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using SD.Responses;
using NUnit.Framework;
using System.Net;
using SD.Hooks;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace SD.Steps
{ 
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        public User[] users;
        public string[] tags;
        private static WebDriverWait _webDriverWait;
        public static int LoginTriesCounter=0;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            tags = _scenarioContext.ScenarioInfo.ScenarioAndFeatureTags;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);

        }


        public static void Login(IWebDriver driver, User user)
        {
                
                _webDriverWait = new WebDriverWait(driver, DriverExtensions._defaultTimeout);

        if (EnvironmentHelper.CurrentStage != Stage.SD_Production){
            EnvironmentHelper.NavigateAndAssertUrl(driver, PageUrls.MockedUserPage);
                WaitUtils.elementState(driver,_webDriverWait, LoginPage.UsersList, ElementState.VISIBLE);
                driver.FindElement(LoginPage.UsersList).Click();
                Thread.Sleep(1000);
                //driver.FindElement(By.XPath("//div[contains(@class, 'dropdown-option') and contains(text(), '"+user.Name+"')][1]")).Click();
                driver.FindElement(By.XPath("//span[contains(@class, 'mat-option') and contains(text(), '"+user.Name+"')][1]")).Click();
                driver.FindElement(LoginPage.UAEPassButton).Click();
                Thread.Sleep(1000);
                EnvironmentHelper.AssertUrl(driver, PageUrls.HomePage);
                driver.Navigate().Refresh();
                // try{
                // Thread.Sleep(1000);
                // WaitUtils.elementState(driver,_webDriverWait, HomePage.UserIcon, ElementState.VISIBLE);
                // driver.FindElement(HomePage.UserIcon).Click();
                // Thread.Sleep(1000);
                // WaitUtils.elementState(driver,_webDriverWait, HomePage.UserName, ElementState.VISIBLE);
                // Thread.Sleep(1000);
                // Assert.IsNotNull(driver.FindElement(HomePage.UserName).Text.ToString().Trim());
                // }
                // catch {
                //  if(LoginTriesCounter<1){
                // LoginTriesCounter++;
                // Login(driver,user);
                //   }
                // }

        }
        else if(EnvironmentHelper.CurrentStage == Stage.SD_Production){
                EnvironmentHelper.NavigateAndAssertUrl(driver, PageUrls.StartPage);
                string at= LoginHelper.GetProductionUserToken(user.Role);
                var script = "localStorage.removeItem('at');localStorage.setItem('at', '"+at+"');";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
                driver.Navigate().Refresh();
                EnvironmentHelper.AssertUrl(driver, PageUrls.HomePage);
        }
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string localStorageValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
            if(localStorageValue.Length<=210 && LoginTriesCounter<1){
                LoginTriesCounter++;
                Login(driver,user);
            }
     
        
        }



        public static void SimplifiedLoginSteps(IWebDriver driver, User user)
        {
                _webDriverWait = new WebDriverWait(driver, DriverExtensions._defaultTimeout);
                EnvironmentHelper.NavigateAndAssertUrl(driver, PageUrls.StartPage);

        if (EnvironmentHelper.CurrentStage != Stage.SD_Production){
            
                WaitUtils.elementState(driver,_webDriverWait, HomePage.UserIcon, ElementState.VISIBLE);
                driver.FindElement(HomePage.UserIcon).Click();
                WaitUtils.elementState(driver,_webDriverWait, LoginPage.UsersList, ElementState.VISIBLE);
                driver.FindElement(LoginPage.UsersList).Click();
                Thread.Sleep(1000);
                //driver.FindElement(By.XPath("//div[contains(@class, 'dropdown-option') and contains(text(), '"+user.Name+"')][1]")).Click();
                driver.FindElement(By.XPath("//span[contains(@class, 'mat-option') and contains(text(), '"+user.Name+"')][1]")).Click();
                driver.FindElement(LoginPage.UAEPassButton).Click();
                Thread.Sleep(1000);
                driver.Navigate().Refresh();
                 Thread.Sleep(1500);

        }
    }



        public static void ApiLogin(User user)
        {
        
        }

        [Then(@"the (.*) will be redirected to Login page")]
        public void ThenTheUserWillBeRedirectedToLoginPage(int Role)
        {
            _webDriver.Url.Should().StartWith("");
        }

        [Then(@"the (.*) will click on logout button")]
        public void ThenTheUserWillClickOnLogoutButton(string userrole)
        {
            By logoutSelector= By.ClassName("user-name");
            By logoutButtonSelector= By.LinkText("Log Off");
      
            _webDriver.FindElement(logoutSelector).Click();
            _webDriver.FindElement(logoutButtonSelector).Click();
        }

        [Given(@"User in Home Page")]
        public void GivenCustomerInCustomerDashboardPage()
        {
        EnvironmentHelper.NavigateAndAssertUrl(_webDriver, PageUrls.StartPage);
        EnvironmentHelper.AssertUrl(_webDriver, PageUrls.HomePage);
        }

              [Given(@"User Scroll down")]
        public void GivenUserScrollDown()
        {
            EnvironmentHelper.ScrollLong(_webDriver);
        }

        [When(@"User Search for the service (.*) and select the service")]
        public void WhenUserSearchForTheServiceAndSelectTheService(string p0)
        {
            WaitUtils.elementState(_webDriver,_webDriverWait, CategoryServicesPage.SearchServiceNew, ElementState.VISIBLE);
            IWebElement searchElement =_webDriver.FindElement(CategoryServicesPage.SearchServiceNew);
            searchElement.SendKeys(p0);
            Assert.AreEqual(searchElement.GetAttribute("value"), _webDriver.FindElement(CategoryServicesPage.FirstSearchResultNew).Text.ToString());
            WaitUtils.elementState(_webDriver,_webDriverWait, CategoryServicesPage.FirstSearchResultNew, ElementState.VISIBLE);
            _webDriver.FindElement(CategoryServicesPage.FirstSearchResultNew).Click();
            Thread.Sleep(2000);
        }

        public static void ReAuthIfNeeded(ScenarioContext scenarioContext,IWebDriver d)
        {
            try
            {
                var user = EnvironmentHelper._currentLoggedInUser;
                IJavaScriptExecutor js = (IJavaScriptExecutor)d;
                string localStorageValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
                if (localStorageValue.Length <= 210)
                {
                    string currentUrl = d.Url;
                    SimplifiedLoginSteps(d, user);
                    d.Navigate().GoToUrl(currentUrl);
                    Thread.Sleep(1000);
                }
            }
            catch
            {
                Console.WriteLine("Failed while try to reauthenticate");
            }
        }

        [Given(@"User in Home Page and opt to login using mobile number")]
        public void UserLogsInUsingMobileNumber()
        {
        EnvironmentHelper.NavigateAndAssertUrl(_webDriver, PageUrls.StartPage);
        EnvironmentHelper.AssertUrl(_webDriver, PageUrls.HomePage);
        
        }


    }
}
