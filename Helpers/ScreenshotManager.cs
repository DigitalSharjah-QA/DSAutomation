using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;

namespace SD.Helpers
{
    public class ScreenshotManager
    {
private static int _sequenceCounter = 0;

static public void TakeScreenshot(IWebDriver driver, ScreenshotType screenshotType = ScreenshotType.Info, string StepName="")
{

try{
            var screenshotTaker = (ITakesScreenshot)driver;

            // Specify the "screenshots" folder
            var screenshotsFolder = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "screenshots");
            var runId = EnvironmentHelper.CurrentStage == Stage.SD_Production ? EnvironmentHelper.ProductionTestRunId : EnvironmentHelper.StaggingTestRunId;

            // Create a subfolder for each test run using the test run ID
            var testRunFolder = Path.Combine(
                screenshotsFolder,
                runId.ToString());

            // Ensure the test run folder exists, create if not
            if (!Directory.Exists(testRunFolder))
            {
                Directory.CreateDirectory(testRunFolder);
            }

            // Specify the path within the test run folder
            var snapshotFile = Path.Combine(
                testRunFolder,
                string.Join('-', EnvironmentHelper.CurrentStage.ToString(), _sequenceCounter.ToString(), screenshotType.ToString(), StepName, ".png"));

            // Save the screenshot to the specified path
            screenshotTaker.GetScreenshot().SaveAsFile(snapshotFile);

            // Add the screenshot file as a test attachment
            TestContext.AddTestAttachment(snapshotFile);

            _sequenceCounter++;
    }
    catch(Exception ex){
        Console.WriteLine("Taking Screenshot Error: "+ex.Message);
    }

}

        private static IntPtr GetWindowHandleBasedOnTitle(string title)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.Contains(title))
                {
                    return proc.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }
    }

    public enum ScreenshotType
    {
        Info,
        Warning,
        Error,
        Critical
    }
}
