using Azure.Identity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace SD.Helpers
{
    public static class DriverManager
    {
        private static IWebDriver _driver;
        public static IWebDriver WebDriver
        {
            get
            {
                if(_driver is null)
                {
                    _driver = DriverSelector.SelectDriverFromEnvironment();
                   //  _driver.Manage().Window.Maximize();
                }

                return _driver;
            }
        }



        public static IJavaScriptExecutor JavaScriptExecutor(this IWebDriver webDriver)
            => (IJavaScriptExecutor)webDriver;

        public static void Cleanup()
        {
            try
            {
                if (_driver is null)
                    return;

                //driver.Quit();// quit does not close the window
                _driver.Close();
                _driver.Dispose();
                _driver = null;

                // Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Drivers"), "chromedriver.exe")));

                // // Terminate each process that is using the target file
                // foreach (Process process in processes)
                // {
                //     if (!process.HasExited)
                //     {
                //         process.Kill();
                //         process.WaitForExit(); // Wait for the process to exit before continuing
                //     }
                // }

            }
            catch (Exception e)
            {
                Console.WriteLine("unable to close the browser", e.Message);
            }
        }
    }
}
