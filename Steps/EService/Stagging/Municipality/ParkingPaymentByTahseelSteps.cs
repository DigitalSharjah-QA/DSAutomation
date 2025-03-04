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

namespace SD.Steps.Eservice.Stagging
{
    [Binding]
    public class ParkingPaymentByTahseelSteps
    {

        private readonly ScenarioContext _scenarioContext;
        public ApiHelper apiHelper;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;

        public string AmountToPay;

        public ParkingPaymentByTahseelSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


        [When(@"User Select Tahseel Payment Method And Click Continue")]
        public void WhenUserSelectTahseelPaymentMethodAndClickContinue()
        {
                       Thread.Sleep(999);
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.ParkingPaymentPage);
             EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.TahseelPaymentMethods, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.TahseelPaymentMethods).Click();
           _webDriver.FindElement(PaymentPage.PaymentContineButton).Click();

        }

        [Then(@"User Enter Tahseel Payment Credentials And Click Pay")]
        public void ThenUserEnterTahseelPaymentCredentialsAndClickPay()
        {
            Thread.Sleep(5000);
            WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.CardNumberInputInTahseelPage, ElementState.VISIBLE);
           
           IWebElement CardNumberInputInTahseelPage=  _webDriver.FindElement(PaymentPage.CardNumberInputInTahseelPage);
           EnvironmentHelper.EnterTextLetterByLetter(CardNumberInputInTahseelPage,EnvironmentHelper.SandboxPaymentMethod);  

           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.PaymentSimulationPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.ACSPaymentSubmitButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.ACSPaymentSubmitButton).Click();
           WaitUtils.elementState(_webDriver,_webDriverWait, ParkingPage.InProgressMessage, ElementState.VISIBLE);
        }

        [Then(@"Success Parking Confirmation Screen Should Appear")]
        public void ThenSuccessParkingConfirmationScreenShouldAppear()
        {
            _scenarioContext.Pending();
        }

    }
}