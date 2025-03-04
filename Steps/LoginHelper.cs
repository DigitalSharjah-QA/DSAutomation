using OpenQA.Selenium;
using SD.Datasources.Users;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq;
using SD.Helpers;
using SD.Datasources;
using SD.Pages;
using RestSharp;
namespace SD.Steps.Helpers
{
    public static class LoginHelper
    {
        


        
        public static void Login(IWebDriver driver, User user)
        {
            LoginSteps.Login( driver,  user);

        }

        public static void ApiLogin(User user)
        {
            LoginSteps.ApiLogin(user); 
        }


           public static string GetProductionUserToken(UserRole userRole)
        {
        // URL to fetch

        // Create RestClient
        var client = new RestClient("https://raw.githubusercontent.com");

        // Create request
        var request = new RestRequest("/SDO-QA/Config/main/Production/SOP3.txt",Method.Get);

        // Execute the request and get the response
        var response = client.Execute(request);

        // Check if the request was successful (status code 200)
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return response.Content.Replace("\n", "");
        }

        else
        {
          
            Console.WriteLine($"Error: {response.StatusCode}");
            return "";
        }
        }





        }
    }