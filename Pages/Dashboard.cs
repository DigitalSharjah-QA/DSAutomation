using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class Dashboard
    {
        public static By TotalRequestLabel = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/p)[1]");
        public static By DoneRequestLabel = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/p)[2]");
        public static By PendingRequestLabel = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/p)[3]");
        public static By RejectedRequestLabel = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/p)[4]");


        public static By TotalRequestCountValue = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/span)[1]");
        public static By DoneRequestCountValue = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/span)[2]");
        public static By PendingRequestCountValue = By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/span)[3]");
        public static By RejectedRequestCountValue= By.XPath("(//div[contains(@class, 'justify-content-around')]/div/div/span)[4]");
         public static By WeatherDegreeValue= By.XPath("(//img[contains(@src, '/weather/')]/parent::div/following-sibling::div/div/p)[1]");    

    }
}
