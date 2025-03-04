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
    public class RoadComplainSteps
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;

        public string AmountToPay;

        public RoadComplainSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


[Then(@"User in Road Complaint Service Catelog Page")]
public void ThenUserinRoadComplaintServiceCatelogPage()
{
	        EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.RoadComplaintCatalogPage);
	        DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.RoadComplaintCatalogPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            IWebElement serviceNameLabel = _webDriver.FindElement(ServiceCatalogPage.ServiceName);
            Assert.AreEqual("شكاوى الطرق", serviceNameLabel.Text.ToString());
            Assert.AreEqual("حسب حالة البلاغ", _webDriver.FindElement(ServiceCatalogPage.ProcedureTime).Text.ToString());
            Assert.AreEqual("مواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString());
            Assert.AreEqual("مقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString());
            Assert.AreEqual("زائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString());

            Assert.AreEqual("مجانا", _webDriver.FindElement(FatwaPage.Fee).Text.ToString());
            Assert.AreEqual(3, _webDriver.FindElements(FatwaPage.StepsCount).Count);
            Assert.AreEqual("التقديم على الطلب", _webDriver.FindElement(FatwaPage.StepOne).Text.ToString());
            Assert.AreEqual("مراجعة الطلب", _webDriver.FindElement(FatwaPage.StepTwo).Text.ToString());
            Assert.AreEqual("إنجاز الخدمة", _webDriver.FindElement(FatwaPage.StepThree).Text.ToString());
            _webDriver.FindElement(ServiceCatalogPage.EntityDetailsTab).Click();
            Assert.AreEqual("هيئة الطرق والمواصلات بالشارقة", _webDriver.FindElement(ServiceCatalogPage.firstEntity).Text.ToString());
            Assert.AreEqual(1, _webDriver.FindElements(ServiceCatalogPage.EntitesList).Count);
            EnvironmentHelper.ScrollTillFind(_webDriver,ServiceCatalogPage.StartServiceButton);            
            _webDriver.FindElement(ServiceCatalogPage.StartServiceButton).Click();
}

        [Then(@"User Submit Road Complaint")]
        public void ThenUserSubmitRoadComplaint()
{
	        DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.RoadComplaintServicePage);
            Thread.Sleep(1000);
            WaitUtils.elementState(_webDriver,_webDriverWait, RoadsAndTransportPage.LocationTextInput, ElementState.VISIBLE);
            IWebElement searchInput= _webDriver.FindElement(RoadsAndTransportPage.LocationTextInput);
            searchInput.Clear();
            EnvironmentHelper.EnterTextLetterByLetter(searchInput,"أمواج الخليج لتجارة");
            Thread.Sleep(1500);
            WaitUtils.elementState(_webDriver, _webDriverWait, RoadsAndTransportPage.FirstSearchResult, ElementState.VISIBLE);
             _webDriver.FindElement(RoadsAndTransportPage.FirstSearchResult).Click();

            WaitUtils.elementState(_webDriver, _webDriverWait, RoadsAndTransportPage.NextButton, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, RoadsAndTransportPage.NextButton);

             _webDriver.FindElement(RoadsAndTransportPage.NextButton).Click();
            
             WaitUtils.elementState(_webDriver,_webDriverWait, RoadsAndTransportPage.MainCategoryList, ElementState.VISIBLE);
             Thread.Sleep(999);
             _webDriver.FindElement(RoadsAndTransportPage.MainCategoryList).Click();
             Thread.Sleep(999);
             WaitUtils.elementState(_webDriver,_webDriverWait, RoadsAndTransportPage.FourthOptionInMainCategoriesList, ElementState.VISIBLE);
             _webDriver.FindElement(RoadsAndTransportPage.FourthOptionInMainCategoriesList).Click();
             Thread.Sleep(999);
            IWebElement DetailsField= _webDriver.FindElement(RoadsAndTransportPage.DetailsTextField);
            EnvironmentHelper.EnterTextLetterByLetter(DetailsField,"يوجد ضرر في الأسفلت");

            IWebElement fileInput = _webDriver.FindElement(By.Id("getFile3"));
            fileInput.SendKeys(AttachmentsPaths.GetPath("attachment.jpg"));
            WaitUtils.elementState(_webDriver,_webDriverWait, FatwaPage.UplaodedAttachemntName, ElementState.VISIBLE);
            EnvironmentHelper.ScrollLong(_webDriver);
            Assert.AreEqual("attachment.jpg",_webDriver.FindElement(FatwaPage.UplaodedAttachemntName).Text);
            EnvironmentHelper.ScrollLong(_webDriver);
            Thread.Sleep(1000);
            IWebElement submitButton;
            try{
             submitButton = _webDriver.FindElement(RoadsAndTransportPage.SubmitButton);
           }
             catch{
             submitButton = _webDriver.FindElement(RoadsAndTransportPage.NextButton);
             }
              ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center' });", submitButton);
            submitButton.Click();
            DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.RoadComplainConfirmationPage);

            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(4));
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.ReferenceNumber, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.TrackYourRequest, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.SubmitAnotherRequest, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);

}
    }
}