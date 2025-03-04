using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SD.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace SD.Helpers
{
    public static class DriverExtensions
    {
        public static readonly TimeSpan _defaultTimeout = EnvironmentHelper.GetTestWaitingTimeout() ?? TimeSpan.FromSeconds(70);

        public static WebDriverWait Wait(this IWebDriver driver, TimeSpan? timeout = null)
            => new WebDriverWait(driver, timeout ?? _defaultTimeout);

        public static void WaitForComponents(this IWebDriver driver, TimeSpan? timeout = null)
        {
            driver
                .Wait(timeout)
                .Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return window.document.hasHomeMounted"));
        }

        public static bool PageLoadingComplete(this IWebDriver driver)
        {
            var jsExecuter = driver.JavaScriptExecutor();
            var keys = new[] { "loaded", "complete" };
            try {
                jsExecuter.ExecuteScript("return document.readyState");
            }
            catch(Exception e) {
                if (e.Message.Contains("result.webdriverValue.value"))
                {
                    Console.WriteLine("Drivers need to be updated: " +e.Message);
                    
                    Task.Run(DriverSelector.UpdateChromeDriversAsync).Wait();
                }
                Console.WriteLine("Line 39: "+ e.Message);
            }
            return keys.Contains(jsExecuter.ExecuteScript("return document.readyState"));
        }

        public static void WaitUntilPageLoaded(this IWebDriver driver, string pageUrl)
        {
            driver.Wait().Until(d => d.PageLoadingComplete() && d.Url.StartsWith(pageUrl));
             EnvironmentHelper.ClickTryAgainIfAppear(driver);
        }
    }
}