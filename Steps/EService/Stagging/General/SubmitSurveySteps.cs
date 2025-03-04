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
    public class SubmitSurveySteps
    {

        private readonly ScenarioContext _scenarioContext;
        public ApiHelper apiHelper;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        public SubmitSurveySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            apiHelper = new ApiHelper(_scenarioContext);
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


  [Then("User Click on Survey Icon")]
    public void ThenUserClicksOnSurveyIcon()
    {
        WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.SupportLink, ElementState.VISIBLE);
        _webDriver.FindElement(HomePage.SupportLink).Click();
        WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.SurveyIcon, ElementState.VISIBLE);
        EnvironmentHelper.ScrollTillFind(_webDriver,HomePage.SurveyIcon);
        _webDriver.FindElement(HomePage.SurveyIcon).Click();
    }

    [Then("User Select (.*)")]
    public void ThenUserSelectsOption(string option)
    {
        Thread.Sleep(2000);
        _webDriver.FindElement(By.XPath("//*[normalize-space(text()) = '"+option+"']")).Click();
    }

    [Then("Select Random Entity")]
    public void AndSelectEntity()
    {
                Thread.Sleep(2000);
        _webDriver.FindElement(SurveyPage.EntitesList).Click();
                        Thread.Sleep(2000);
        _webDriver.FindElement(SurveyPage.ParticularEntity).Click();
    }

    [When("User fill the Survey Form")]
    public void WhenUserFillsSurveyForm()
    {
        Thread.Sleep(2000);
        _webDriver.FindElement(SurveyPage.ServicesList).Click();
                        Thread.Sleep(2000);
        _webDriver.FindElement(SurveyPage.FirstService).Click();
                EnvironmentHelper.ScrollTillFind(_webDriver,SurveyPage.SDoChannel);
        _webDriver.FindElement(SurveyPage.SDoChannel).Click();
                EnvironmentHelper.Scroll(_webDriver);
                if(EnvironmentHelper.CurrentStage==Stage.SD_Stagging){
         IWebElement ServiceName=  _webDriver.FindElement(SurveyPage.ServiceName);
         EnvironmentHelper.EnterTextLetterByLetter(ServiceName,"All Services"); 
                Thread.Sleep(1000);
                }
                Thread.Sleep(2000);
        _webDriver.FindElement(SurveyPage.FirstReason).Click();
        EnvironmentHelper.Scroll(_webDriver);
    }

    [When("User submit the Survey")]
    public void AndUserSubmitsSurvey()
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].click();",  _webDriver.FindElement(SurveyPage.SubmitButton));

    }


    [Then("User should see the Survey Submission Success Message")]
    public void ThenUserSeesSuccessMessage()
    {
        if(EnvironmentHelper.CurrentStage==Stage.SD_Production){
      WaitUtils.elementState(_webDriver,_webDriverWait, SurveyPage.ProdSuccessMessage, ElementState.VISIBLE);
        }else{
      WaitUtils.elementState(_webDriver,_webDriverWait, SurveyPage.StaggingSuccessMessage, ElementState.VISIBLE);
        }
      WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
      Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(1));
      WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.SubmitAnotherSurvey, ElementState.VISIBLE);

  }


    }
}
