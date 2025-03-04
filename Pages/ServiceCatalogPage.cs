using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.Profiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class ServiceCatalogPage
    {
        public static By ServiceName = By.XPath("(//p[contains(@class, 'header')])[1]");
         public static By ProcedureTime = By.XPath("(//p[text()='وقت الإجراءات' or text()='Processing Time']//following::p)[1]");
         public static By  TargetAudienceFirstCategory  = By.XPath("(//p[text()='Target Audience' or 'الجمهور المستهدف']//following::ul[contains(@class,'lh-sm mb-0')]/li/p)[1]");
         public static By  TargetAudienceSecondCategory  = By.XPath("(//p[text()='Target Audience' or 'الجمهور المستهدف']//following::ul[contains(@class,'lh-sm mb-0')]/li/p)[2]");
         public static By  TargetAudienceThirdCategory  = By.XPath("(//p[text()='Target Audience' or 'الجمهور المستهدف']//following::ul[contains(@class,'lh-sm mb-0')]/li/p)[3]");

        public static By StepOne = By.XPath("//p[@class='mainColor fontFamilyRegular mb-0']");

        public static By EntityDetailsTab = By.XPath("(//li/a[contains(text(),'Entity Details') or contains(text(),'مقدمي الخدمة')])[2]");
         public static By EntitesList = By.XPath("//img[contains(@class,'logoDimensions')]/following::p[1]");
         public static By firstEntity = By.XPath("(//img[contains(@class,'logoDimensions')]/following::p[1])[1]");
         public static By secondEntity = By.XPath("(//img[contains(@class,'logoDimensions')]/following::p[1])[2]");
         public static By ThirdEntity = By.XPath("(//img[contains(@class,'logoDimensions')]/following::p[1])[3]");
         public static By FourthEntity = By.XPath("(//img[contains(@class,'logoDimensions')]/following::p[1])[4]");


         public static By PaymentMethods = By.XPath("//span[@class='mainColor fontSize14']");
         public static By firstPaymentMethod = By.XPath("(//span[@class='mainColor fontSize14'])[1]");
         public static By secondPaymentMethod = By.XPath("(//span[@class='mainColor fontSize14'])[2]");
        public static By thirdPaymentMethod = By.XPath("(//span[@class='mainColor fontSize14'])[3]");

         public static By FeesList = By.XPath("(//div[@class='row g-0 feesRow p-2 mb-3'])");

         public static By FeesValues = By.XPath("(//div[@class='row g-0 feesRow p-2 mb-3'])/div[2]/a");

        public static By StartServiceButton = By.XPath("//button[contains(text(),'Start Service') or contains(text(),'بدء الخدمة')]");

        public static By ParkingName = By.XPath("//div[contains(@class,'justify-content-between align-items-center')]/div/p[contains(@class,'header')]");
    }
    
}
