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
    public class GenTokenSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private JObject jsonResponse;
        private RestResponse response;
        private static WebDriverWait _webDriverWait;
        public string dsToken, localStorageValue ;
        private string firstNameResponse;


        public GenTokenSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            jsonResponse = JObject.Parse("{}");
            response = null;
            //otpCode= localStorageValue ="";
        }


        [Given(@"I request and receive the DSToken")]
        public void WhenIRequestAndReceiveTheDSToken()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
             //localStorageValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
            // initite the request 
            var client = new RestClient(EnvironmentHelper.ApiBaseUrl);
            var request = new RestRequest("securityservices/api/api/security/login", Method.Post);

            // add haeders
            request.AddHeader("language", "en");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("dstoken", localStorageValue);
 
            //body
            request.AddBody(
                        new{
                            userData = new{
                            idType = "IL",
                            sub = "KSJMNCDS-98899-89461369-56408-8563E259CAA9D",
                            lastnameEN = "Sharjah",
                            firstnameEN = "Digital",
                            idn = "784202169973799",
                            userType = "SOP3",
                            email = "ds.sharjah@gmail.com",
                            fullnameEN = "digital sharjah",
                            domain = "digitalid-users",
                            nationalityEN = "ARE",
                            gender = "Male",
                            uuid = "KSJMNCDS-98899-89461369-56408-8563E259CAA9D",
                            mobile = "97155200000899"
                                                },
                            authinticated = true,
                            isExp = "F"
                        });

            //execute
            Thread.Sleep(999);
            response = client.ExecutePostAsync(request).Result;
            jsonResponse = JObject.Parse(response.Content);
            Thread.Sleep(999);
            dsToken = jsonResponse["token"].ToString();
            firstNameResponse = jsonResponse["profile"]["firstnameEN"].ToString();

            //Assertions
            try
            {
               Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }

            try
            {
               Assert.That(firstNameResponse, Is.EqualTo("Digital"));
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }
        }
    }
}