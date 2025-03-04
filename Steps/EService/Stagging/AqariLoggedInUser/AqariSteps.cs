using TechTalk.SpecFlow;
using SD.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SD.Pages;
using SD.Helpers.Enums;
using System.Threading;
using NUnit.Framework;
using SD.Datasources.Attachments;


namespace SD.Steps.Eservice.Stagging
	{
		[Binding]
public class AquariSteps
{

 private readonly ScenarioContext _scenarioContext;
    public ApiHelper apiHelper;
    private readonly IWebDriver _webDriver;
    private readonly WebDriverWait _webDriverWait;
     
    public string AmountToPay;

    public AquariSteps(ScenarioContext scenarioContext) {
      _scenarioContext = scenarioContext;
      _webDriver = DriverManager.WebDriver;
      _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);

    }



[When(@"User validates the Aqari Page")]
public void ThenUserinAqariPage()
{
  WaitUtils.elementState(_webDriver, _webDriverWait, AqariPage.AqariIcon, ElementState.VISIBLE);
  EnvironmentHelper.ScrollTillFind(_webDriver, AqariPage.AqariIcon);
  _webDriver.FindElement(AqariPage.AqariIcon).Click();
  Thread.Sleep(10000);
 string currentUrl = _webDriver.Url;
 Assert.AreEqual("https://stg-aqari.ds.sharjah.ae/", currentUrl);
        }

[Then(@"User validates aqari from the activity page")]
public void UserChecksAqariInActivityPage()
{
  WaitUtils.elementState(_webDriver, _webDriverWait, AqariPage.AqariHome, ElementState.VISIBLE);
  _webDriver.FindElement(AqariPage.AqariHome).Click();
  Thread.Sleep(10000);
 string currentUrl = _webDriver.Url;
 Assert.AreEqual("https://stg-ds.sharjah.ae/home", currentUrl);
   WaitUtils.elementState(_webDriver, _webDriverWait, HomePage.UserIcon, ElementState.VISIBLE);
 _webDriver.FindElement(HomePage.UserIcon).Click();
  WaitUtils.elementState(_webDriver, _webDriverWait, HomePage.UserActivity, ElementState.VISIBLE);
 _webDriver.FindElement(HomePage.UserActivity).Click();
  WaitUtils.elementState(_webDriver, _webDriverWait, AqariPage.ActivitySearchbox, ElementState.VISIBLE);
  
  IWebElement searchbox=  _webDriver.FindElement(AqariPage.ActivitySearchbox);
  EnvironmentHelper.EnterTextLetterByLetter(searchbox,"Power of Attorney"); 
  WaitUtils.elementState(_webDriver, _webDriverWait, AqariPage.ActivityAqariItem, ElementState.VISIBLE);
   _webDriver.FindElement(AqariPage.ActivityAqariItem).Click();
   Thread.Sleep(10000);
 string currentUrlagain = _webDriver.Url;
 Assert.AreEqual("https://stg-aqari.ds.sharjah.ae/activities?searchBy=application&applicationType=1007", currentUrlagain);  
  WaitUtils.elementState(_webDriver, _webDriverWait, AqariPage.AqariHome, ElementState.VISIBLE);
  _webDriver.FindElement(AqariPage.AqariHome).Click();
  Thread.Sleep(10000);
 string currentUrlnew = _webDriver.Url;
 Assert.AreEqual("https://stg-ds.sharjah.ae/home", currentUrlnew);
        }
    }
}