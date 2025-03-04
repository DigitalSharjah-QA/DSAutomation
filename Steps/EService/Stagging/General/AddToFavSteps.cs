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
    public class AddToFavSteps
    { 

        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        private string ServiceName;

        public AddToFavSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


 
        [Given(@"User Navigate to First Service")]
        public void GivenUserNavigatetoFirstService()
        {
            Thread.Sleep(1000);
            EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.FirstServiceCatalogPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            ServiceName=_webDriver.FindElement(ServiceCatalogPage.ServiceName).Text;

        }

        [When(@"User in Service catalog Page He Add The Service to Favourites")]
        public void WhenUserinServicecatalogPageHeAddTheServicetoFavourites()
        {
            Thread.Sleep(1999);
            WaitUtils.elementState(_webDriver,_webDriverWait, AddToFavPage.AddToFavIcon, ElementState.VISIBLE);
            _webDriver.FindElement(AddToFavPage.AddToFavIcon).Click();
        }

        [Then(@"Serive Should Appear in the Favourites Services")]
        public void ThenSeriveShouldAppearintheFavouritesServices()
        {
             EnvironmentHelper.NavigateAndAssertUrl(_webDriver, PageUrls.HomePage);
             WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.FavouriteServicesLabel, ElementState.VISIBLE);
             WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.FirstFavouriteServiceName, ElementState.VISIBLE);
             Assert.AreEqual(ServiceName, _webDriver.FindElement(HomePage.FirstFavouriteServiceName).Text.ToString());

        }

        [Then(@"User remove it from Favourites")]
        public void ThenUserremoveitfromFavourites()
        {
            _webDriver.FindElement(HomePage.FirstFavouriteServiceName).Click();
             WaitUtils.elementState(_webDriver,_webDriverWait, AddToFavPage.AddToFavIcon, ElementState.VISIBLE);
            _webDriver.FindElement(AddToFavPage.AddToFavIcon).Click();
             EnvironmentHelper.NavigateAndAssertUrl(_webDriver, PageUrls.HomePage);
             WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.FavouriteServicesLabel, ElementState.NOT_VISIBLE);
             WaitUtils.elementState(_webDriver,_webDriverWait, HomePage.FirstFavouriteServiceName, ElementState.NOT_VISIBLE);
        }


    }
}
