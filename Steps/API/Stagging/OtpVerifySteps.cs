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
    public class OtpVerifySteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;
        private JObject jsonResponse;
        private RestResponse response;
        private static WebDriverWait _webDriverWait;
        private string otpCode, localStorageValue ;


        public OtpVerifySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            jsonResponse = JObject.Parse("{}");
            response = null;
            otpCode= localStorageValue ="";
        }



        [When(@"I request and receive an OTP via the request OTP API")]
        public void WhenIrequestandreceiveanOTPviatherequestOTPAPI()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
             localStorageValue = (string)js.ExecuteScript("return localStorage.getItem('at');");
            // initite the request 
            var client = new RestClient(EnvironmentHelper.ApiBaseUrl);
            var request = new RestRequest("online/generalservices/general-services/otp", Method.Post);

            // add haeders
            request.AddHeader("language", "ar");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("dstoken", localStorageValue);
 
            //body
            request.AddJsonBody(
                        new
                        {
                            mobile = "0509821544",
                            enableExtraDigits = true
                        });


            //execute
            Thread.Sleep(999);
            response = client.ExecutePostAsync(request).Result;
            jsonResponse = JObject.Parse(response.Content);
            Thread.Sleep(999);
             otpCode = jsonResponse["data"]["otp"].ToString();

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

        [Then   (@"I validate the received OTP via the validate OTP API")]
        public void GivenIvalidatethereceivedOTPviathevalidateOTPAPI()
        {
                         // initite the request 
            var client = new RestClient(EnvironmentHelper.ApiBaseUrl);
            var request = new RestRequest("online/generalservices/general-services/otp-verify", Method.Post);

            // add haeders
            request.AddHeader("language", "ar");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("dstoken", localStorageValue);
 
            //body
            request.AddJsonBody(
                        new
                        {
                            mobile = "0509821544",
                            otp = otpCode
                        });


            //execute
            Thread.Sleep(999);
            response = client.ExecutePostAsync(request).Result;
            Thread.Sleep(999);
            jsonResponse = JObject.Parse(response.Content);
           
            //Assertions

                    // Extracting values from the JSON response
                string errorMessage = jsonResponse["result"]["errorMessage"].ToString();
                string errorFlag = jsonResponse["result"]["errorFlag"].ToString();
                string statusCode = jsonResponse["result"]["statusCode"].ToString();
                string result = jsonResponse["data"]["result"].ToString();
                string referenceNo = jsonResponse["data"]["referenceNo"].ToString();


                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(errorMessage, Is.EqualTo("OK"));
                Assert.That(errorFlag, Is.EqualTo("F"));
                Assert.That(statusCode, Is.EqualTo("200"));
                Assert.That(result, Is.EqualTo("SUCCESS"));
                Assert.That(referenceNo.Length, Is.InRange(5, 6));
                Assert.That(referenceNo, Does.Match(@"^\d{5,6}$"));

   
        }

    }
}
