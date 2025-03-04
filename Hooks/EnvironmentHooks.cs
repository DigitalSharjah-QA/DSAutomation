using AngleSharp.Text;
using OpenQA.Selenium;
using SD.Helpers;
using SpecFlow.Internal.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SD.Hooks
{
    [Binding]
    public class EnvironmentHooks
    {
        public static string env = "web";
        public static string CurrentStep = "";
       public static List<string> testResults = new List<string>();

        [BeforeFeature(Order = -int.MaxValue)]

        public static void SetFeatureEnvironment(FeatureContext featureContext)
        {
            var stage = Stage.Unknown;

        var tags = featureContext.FeatureInfo.Tags;
          
            if (tags.Contains("SD_Stagging")) stage = Stage.SD_Stagging;
            else if (tags.Contains("SD_Production")) stage = Stage.SD_Production;
           

            EnvironmentHelper.SetStage(stage);
            if (tags.Contains("Api_Authorized_Customer")) env = "api";

        }

        [BeforeScenario(Order = -int.MaxValue)]
        public static void SetScenarioEnvironment(ScenarioContext scenarioContext)
        {
            var stage = Stage.Unknown;
            var tags = scenarioContext.ScenarioInfo.Tags;
            if (tags.Contains("SD_Stagging")) stage = Stage.SD_Stagging;
            else if (tags.Contains("SD_Production")) stage = Stage.SD_Production;
            if (tags.Contains("Api_Authorized_Customer")) env = "api";

            if (EnvironmentHelper.CurrentStage == Stage.Unknown)
            {
                  EnvironmentHelper.SetStage(stage);
            }
        }


        [BeforeScenario(Order = 1)]
        public static void ReAuthIfNeeded(ScenarioContext scenarioContext)
        {
            var tags = scenarioContext.ScenarioInfo.Tags;
            if(tags.Contains("Authorized_Customer_SOP1") ||tags.Contains("Authorized_Customer_SOP2")||tags.Contains("Authorized_Customer_SOP3")){
                Steps.LoginSteps.ReAuthIfNeeded(scenarioContext,DriverManager.WebDriver);
            }
        }




        [AfterScenario(Order = -3)]
        public static void ReflectStatusOnTestRailRun(ScenarioContext scenarioContext)
        {
        string msg=null;
        ulong.TryParse(scenarioContext.ScenarioInfo.Description,out ulong testCaseId);
        TestCaseStatus result= TestCaseStatus.Passed;
        if(scenarioContext.TestError != null ){
                result= TestCaseStatus.Failed;
                
                 msg=scenarioContext.TestError.Message.ToString();                
        }
        EnvironmentHelper.UpdateTestcaseStatus(testCaseId, result, msg); 
        }



        [AfterScenario(Order = -5)]
        public static void ScreenshotError(ScenarioContext scenarioContext)
        {
        var scenarioResult = scenarioContext.TestError == null ? "Passed" : "Failed";

         testResults.Add(scenarioResult);

            if (scenarioContext.TestError != null && env != "api")
            {
            
                ScreenshotManager.TakeScreenshot(DriverManager.WebDriver, ScreenshotType.Error, CurrentStep);

            }

        }

        [AfterScenario(Order = 1000)]
        public static void CleanDriver(ScenarioContext scenarioContext)
        {

            DriverManager.Cleanup();
        }



        [AfterStep]
        public static void AfterStepHook(ScenarioContext scenarioContext)
        {
         CurrentStep= scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString()+ " " + scenarioContext.StepContext.StepInfo.Text.ToString();
        }


     [BeforeTestRun]
    public static void Beforetest()
    {
      EnvironmentHelper.AddTestrailRun();
    }

     [AfterTestRun]
    public static async Task AfterTestRunAsync()
    {
        await SlackHelper.GenerateLivingDocReport();
        
           try{            

        int passedCount = testResults.Count(result => result == "Passed");
        int failedCount = testResults.Count(result => result == "Failed");
        await SlackHelper.UploadFileToSlack(passedCount,failedCount);
           }
            catch(Exception e){

              Console.WriteLine(e.Message);
           }
          EnvironmentHelper.CloseTestrailRun();
    }


  }
}