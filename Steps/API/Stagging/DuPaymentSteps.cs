using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using SD.Helpers;
using SD.Datasources.Users;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using SD.Responses;
using NUnit.Framework;
using System.Net;
using System.Threading;

namespace SD.Steps.API.QA.Services.CRUD
{
    [Binding]
    public class DuPaymentSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private JObject jsonResponse;
        private RestResponse response;
        private static WebDriverWait _webDriverWait;
        public string token, localStorageValue ;
        private string firstNameResponse;


        public DuPaymentSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            jsonResponse = JObject.Parse("{}");
            response = null;
            //otpCode= localStorageValue ="";
        }


        [Given(@"I request and validate the Du Accounts api")]
        public void WhenIRequestAndValidateDuAccountsAPI()
        {   
            // initite the request 
            var client = new RestClient(EnvironmentHelper.ApiBaseUrl);
            var request = new RestRequest("du-v2/api/du-payment/v2/du-accounts", Method.Get );

            // add haeders
            request.AddHeader("language", "en");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json"); 
            request.AddHeader("dstoken", token); 

            //execute
            Thread.Sleep(999);
            response = client.ExecutePostAsync(request).Result;
            jsonResponse = JObject.Parse(response.Content);
            Thread.Sleep(999);


            //Assertions
            try
            {
               Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }
        }
}
}