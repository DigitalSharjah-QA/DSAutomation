using OpenQA.Selenium;
using SD.Datasources.Users;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using RestSharp;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SD.Helpers
{
    public  class ApiHelper
    {
      public static Random random = new Random();
      public RestResponse services;
      private readonly ScenarioContext _scenarioContext;


        public ApiHelper(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }





        public static bool getRandomBoolean()
        {
            return random.Next(2) == 1;
        }


    }
}