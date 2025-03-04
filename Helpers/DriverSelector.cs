using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SD.Helpers
{
    static class DriverSelector
    {

        public static IWebDriver SelectDriverFromEnvironment()
        {
            var x=EnvironmentHelper.GetSeleniumDriver()?.Trim()?.ToLower();
            return (EnvironmentHelper.GetSeleniumDriver()?.Trim()?.ToLower()) switch
            {
                "firefox_driver" => FirefoxDriver,
                "edge_driver" => EdgeDriver,
                "ie_driver" => InternetExplorerDriver,
                _ => ChromeDriver,
            };
        }

        private static string DriversDirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), "Drivers");

        private readonly static PageLoadStrategy _pageLoadStrategy = PageLoadStrategy.Eager;
        private static FirefoxDriver FirefoxDriver
        {
            get
            {
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());

                var service = FirefoxDriverService.CreateDefaultService(DriversDirectoryPath);
                service.Host = "::1";

                var options = new FirefoxOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new FirefoxDriver(DriversDirectoryPath, options);
            }
        }
        private static EdgeDriver EdgeDriver
        {
            get
            {
                var options = new EdgeOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new EdgeDriver(DriversDirectoryPath, options);
            }
        }
        private static InternetExplorerDriver InternetExplorerDriver
        {
            get
            {
                var options = new InternetExplorerOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                };

                return new InternetExplorerDriver(DriversDirectoryPath, options);
            }
        }

        private static ChromeDriver ChromeDriver
        {
            get
            {
                var options = new ChromeOptions
                {
                    PageLoadStrategy = _pageLoadStrategy,
                    
                };
   
                options.AddArgument("no-sandbox");
               // options.AddArgument("--headless"); 
                //options.AddArguments("--use-fake-ui-for-media-stream");
                //options.AddArguments("--use-fake-device-for-media-stream");
                //Set specific capabilities to allow geolocation permissions
                //options.AddArgument("--enable-features=AllowLocationOnFileUrls");
                //options.AddArgument("--enable-features=AllowLocationForUrls");
                //options.AddArgument("--enable-features=AllowGeolocationForUrls");
                //options.AddArgument("--disable-notifications");
                //options.AddArgument("--enable-features=Geolocation");
                //options.AddArguments("--enable-features=AllowLocationOnFileUrls");
                //options.AddArgument("--window-size=1909,1150");
                //options.AddArgument("--geolocation=25.277482,55.680448");
                //options.AddArgument("--remote-debugging-port=56789");

                Dictionary<string, int> contentSettings = new Dictionary<string, int>();
                Dictionary<string, object> profile = new Dictionary<string, object>();
                Dictionary<string, object> prefs = new Dictionary<string, object>();

                contentSettings.Add("notifications", 1);
                contentSettings.Add("geolocation", 1);
                profile.Add("managed_default_content_settings", contentSettings);
                profile.Add("default_content_setting_values", contentSettings);
                prefs.Add("profile", profile);
                options.AddUserProfilePreference("prefs", prefs);
               // options.AddArgument("--start-fullscreen");
               // options.AddArgument("--start-maximized");
                //options.AddArgument("--incognito");
                options.AddArgument("--window-size=1935,1150");
                options.AddArgument("--window-position=0,0");
                options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;

                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                 try
                 {
                    var driver =new ChromeDriver(DriversDirectoryPath, options);
                    
                    driver.ExecuteCdpCommand("Browser.grantPermissions", new Dictionary<string, object>
                    {
                        ["origin"] = EnvironmentHelper.WebsiteRootPageUrl,
                        ["permissions"] = new string[] { "geolocation" }
                    });

                    // Set geolocation override
                    driver.ExecuteCdpCommand("Emulation.setGeolocationOverride", new Dictionary<string, object>
                    {
                        ["latitude"] = 25.346254,
                        ["longitude"] = 55.420933,
                        ["accuracy"] = 100
                    });

                    return driver;
                 }
                 catch (Exception e) {
                    
                         Console.WriteLine("Issue Initianing Driver: " + e.Message);
                         Task.Run(UpdateChromeDriversAsync).Wait();
                 }

                return new ChromeDriver(DriversDirectoryPath, options);
            }
        }


       public static async Task UpdateChromeDriversAsync()
        {
            // Step 1: Create an instance of ChromeDriverUpdater
            ChromeDriverUpdater updater = new ChromeDriverUpdater();

            // Step 2: Call the UpdateChromeDriverAsync() method
            await updater.UpdateChromeDriverAsync();
        }
    }
}



