using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SD.Datasources.Users;
using TestRail;
using TestRail.Types;
using Newtonsoft.Json;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using SD.Datasources.Attachments;
using System.Threading;
using SD.Steps;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Interactions;
namespace SD.Helpers  
{
    public static class EnvironmentHelper
    {
        
        public static string WebsiteRootPageUrl => _stage switch
        {

            Stage.SD_Stagging => "https://stg-ds.sharjah.ae/",
            Stage.SD_Production => "https://ds.sharjah.ae/",

            _ => "UNKNOWN",
        };

        public static string ApiBaseUrl => _stage switch
        {
            Stage.SD_Stagging => "https://stg-ds.sharjah.ae/",
            Stage.SD_Production => "https://ds.sharjah.ae/",
            _ => "UNKNOWN",
        };
        
        public static string TestrailBaseUrl="https://sdo.testrail.io/"; 
        public static string TestrailUsername="fayez.alibrahim@sdo.shj.ae";
        public static string TestrailApiKey="Ly2ErI6FYf137bzcY0Wy-DiqrZMMz287GLHrwwmkK";

        //public static string slackToken = "xoxb-4914961415651-6896615263586-FFNtvI8sXG50NxQOmGKDERv1";
        public static string channel = "C06G7KZ23J6";

        public static string SandboxPaymentMethod = "4242424242424242";

        public static string LivingDocfilePath =@"./../../../LivingDoc.html";

       // public static ulong CurrentTestRunId = 0;
        public static ulong ProductionTestRunId = 0;
        public static ulong StaggingTestRunId = 0;

        public static bool ProdRunIsEmpty = true;
        public static bool StaggingRunIsEmpty = true;

        private static Stage _stage = Stage.Unknown;
        public static Datasources.Users.User _currentLoggedInUser = new Datasources.Users.User();
        public static Stage CurrentStage => _stage;
        public static void SetStage(Stage stage) => _stage = stage;
        public static By dialogLocator = By.XPath("//button[contains(text(), 'المحاولة مرة أخرى')]");
        public static By MobileNumberFieldForGuestUser = By.XPath("//input[@formcontrolname='mobileNumber']");
        public static By LocationPopupLabel = By.XPath("//p[contains(text(), 'تفعيل خاصية تحديد الموقع')]");
        public static By LocationPopupbutton = By.XPath("//button[contains(text(), 'المحاولة مرة أخرى')]");
        public static By LocationPopupbCancel = By.XPath("//button[contains(text(), 'إلغاء')]");
         public static TestRailClient client = GetTestrailClient();
        public static string GetSeleniumDriver()
            => Environment.GetEnvironmentVariable("SELENIUM_DRIVER");

        public static TimeSpan? GetTestWaitingTimeout()
        {
            var secondsSrt = Environment.GetEnvironmentVariable("TEST_RENDER_TIMEOUT");
            if (string.IsNullOrEmpty(secondsSrt)) return null;
            else return TimeSpan.FromSeconds(Convert.ToInt32(secondsSrt));
        }

        public static void NavigateAndAssertUrl(IWebDriver _webDriver, string url)
        {
            _webDriver.Navigate().GoToUrl(url);
            AssertUrl(_webDriver, url);
        }
 
        private static TestRailClient GetTestrailClient()
        {
          TestRailClient client = new TestRailClient(TestrailBaseUrl,TestrailUsername,TestrailApiKey); 
          return client;
        }

        public static TestRail.Utils.RequestResult<Run> AddTestrailRun(ulong projectId=1,ulong suitId=75,string testRunName = "Automated Test Run | ",string testRunDescription = "This test run is created using the TestRail API.")
        {
                var v= client.GetMilestones(projectId);
                var activeMilestones = v.Payload.Where(milestone => milestone.Name == "AUTOMATION").ToList();
                var firstMilestone = activeMilestones[0];
                ulong milestoneId=firstMilestone.Id;
            DateTime currentLocalDateTime = DateTime.Now;

            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dubai");

            currentLocalDateTime = TimeZoneInfo.ConvertTime(currentLocalDateTime, TimeZoneInfo.Local, targetTimeZone);


               HashSet<ulong> StaggingCasesIds = new HashSet<ulong> { 5890, 5891, 5892, 9227, 5893, 6840, 5894,12111,12464 ,12573,12913,15159};
               HashSet<ulong> ProductionCasesIds = new HashSet<ulong> { 12439,6413, 5887, 5888, 5889, 8087 ,12634,13253};


                TestRail.Utils.RequestResult < Run > ProductionTestRun = client.AddRun(projectId, suitId, "Production automated Test | "+ currentLocalDateTime.ToString("dd MMMM hh:mm tt"), testRunDescription,milestoneId,null,ProductionCasesIds);
                TestRail.Utils.RequestResult < Run > StaggingTestRun = client.AddRun(projectId, suitId, "Stagging automated Test | "+ currentLocalDateTime.ToString("dd MMMM hh:mm tt"), testRunDescription,milestoneId,null,StaggingCasesIds);

                dynamic parsedProdResult = JsonConvert.DeserializeObject(ProductionTestRun.RawJson);
                dynamic parsedStaggingResult = JsonConvert.DeserializeObject(StaggingTestRun.RawJson);

                ProductionTestRunId=(ulong)parsedProdResult.SelectToken("id");
                StaggingTestRunId=(ulong)parsedStaggingResult.SelectToken("id");

                return ProductionTestRun;
        }



        public static void UpdateTestcaseStatus(ulong testCaseId, TestCaseStatus status, string ErrorMsg = null)
        {
            if (CurrentStage == Stage.SD_Production)
            {
                client.AddResultForCase(ProductionTestRunId, testCaseId, (TestRail.Enums.ResultStatus?)status, ErrorMsg, null, null, null, 9, null);
                ProdRunIsEmpty = false;
            }
            else
            {
                client.AddResultForCase(StaggingTestRunId, testCaseId, (TestRail.Enums.ResultStatus?)status, ErrorMsg, null, null, null, 9, null);
                StaggingRunIsEmpty = false;
            }
        }

        public static void CloseTestrailRun()
        {
            if (ProdRunIsEmpty == true)
            {
                client.DeleteRun(ProductionTestRunId);
            }
            else
            {
                client.CloseRun(ProductionTestRunId);

            }



            if (StaggingRunIsEmpty == true)
            {
                client.DeleteRun(StaggingTestRunId);
            }
            else
            {
                client.CloseRun(StaggingTestRunId);

            }
        }

        public static void AssertUrl(IWebDriver _webDriver, string url)
        {
            _webDriver.WaitUntilPageLoaded(url);
            _webDriver.Url.Should().StartWith(url);
        }

           public static void ScrollLong(IWebDriver _webDriver) 
        {
           ((IJavaScriptExecutor)_webDriver).ExecuteScript($"window.scrollTo(0, 9999)");
           Thread.Sleep(500);
        }

        public static void Scroll(IWebDriver _webDriver) 
        {
            var js = (IJavaScriptExecutor)_webDriver;           
            js.ExecuteScript(" window.scrollTo(0, window.scrollY + 500);");
        }

        public static void ClickTryAgainIfAppear(IWebDriver _webDriver)
        {
            if (_webDriver.FindElements(dialogLocator).Count > 0)
            {
                _webDriver.FindElement(dialogLocator).Click();
                Thread.Sleep(2000);
                    if (_webDriver.FindElements(dialogLocator).Count > 0)
                    {
                        _webDriver.FindElement(dialogLocator).Click();
                        Thread.Sleep(2000);
                    }
            }
        }

        public static void CheckIfLocationPromptAppear(IWebDriver _webDriver)
        {
            if (_webDriver.FindElements(LocationPopupLabel).Count > 0 && _webDriver.FindElements(LocationPopupbutton).Count > 0)
            {
                _webDriver.FindElement(LocationPopupbCancel).Click();
                Thread.Sleep(1000);
            }
        }


        public static void HandleIfLoginNotComplete(IWebDriver _webDriver)
        {
            if (_webDriver.FindElements(MobileNumberFieldForGuestUser).Count > 0)
            {
            string currentUrl = _webDriver.Url;
            LoginSteps.SimplifiedLoginSteps(_webDriver,_currentLoggedInUser);
             _webDriver.Navigate().GoToUrl(currentUrl);
            }
        }


            public static void EnterTextLetterByLetter(IWebElement element, string text)
    {
             foreach (char character in text)
                {
                    element.SendKeys(character.ToString());
                    Thread.Sleep(100);
                }
    }
    
//            public static bool IsAlertPresent(IWebDriver driver)
//     {
//         try
//         {            
//         var frames = driver.FindElements(By.TagName("iframe"));
// var y=frames.Count;
//         foreach (var frame in frames)
//         {
//           var x= frame.GetAttribute("name") ?? frame.GetAttribute("id");
//         }

//             //driver.SwitchTo().Alert().Accept();
//              driver.SwitchTo().Frame("settings");

//             return true;
//         }
//         catch (NoAlertPresentException e)
//         {
//             var x=e.Message;
//             return false;
//         }
//     }

    public static IWebElement ScrollTillFind(IWebDriver driver, By locator,int scrollAmount=500)
    {
        int maxScrolls = 5; // Adjust the maximum number of scrolls as needed
        int currentScrolls = 0;
        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
        while (currentScrolls < maxScrolls)
        {
            try
            {
                // Attempt to find the element
                IWebElement element = driver.FindElement(locator);
    
                // If the element is found, return it
                if (element != null && element.Displayed && driver.FindElements(locator).Count>0)
                {
               //  jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
               jsExecutor.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center' });", element);
               Thread.Sleep(1999);
                    return element;
                }
            }
            catch (NoSuchElementException)
            {
                // If the element is not found, scroll and try again 
                jsExecutor.ExecuteScript($"window.scrollBy(0, {scrollAmount})");
                // Increment the scroll count
                currentScrolls++;
                // Wait for a short duration to allow the content to load after scrolling
                Thread.Sleep(500);
            }
        }

        // If the element is not found after maximum scrolls, return null
        return null;
    }

    }




    public enum Stage
    {
        Unknown,
        SD_Stagging = 1,
        SD_Production = 2,
    }


    public enum TestCaseStatus{
        Passed=TestRail.Enums.ResultStatus.Passed,
        Blocked=TestRail.Enums.ResultStatus.Blocked,
        Untested=TestRail.Enums.ResultStatus.Untested,
        Retest=TestRail.Enums.ResultStatus.Retest,
        Failed=TestRail.Enums.ResultStatus.Failed
    }
}
