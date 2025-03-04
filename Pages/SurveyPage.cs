using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class SurveyPage
    {
        public static By EntitesList = By.XPath("(//div[contains(@class, 'mat-select-arrow')])[1]");
        public static By ServicesList = By.XPath("(//div[contains(@class, 'mat-select-arrow')])[3]");

        public static By FirstService = By.XPath("(//span[@class='mat-option-text'])[2]");

        public static By ServiceNameInput = By.XPath("//input[@formcontrolname='serviceName' and contains(@class, 'mat-input-element')]");
        public static By SDoChannel = By.XPath("//span[normalize-space(text())='منصة الشارقة الرقمية']");

        public static By FirstReason = By.XPath("//span[@class='mat-checkbox-label' and normalize-space(text())='إجراءات الخدمة']");
        public static By ServiceName = By.XPath("//input[@formcontrolname='serviceName']");

        public static By SubmitButton = By.XPath("//button[normalize-space(text())='تقديم']");

        public static By StaggingSuccessMessage = By.XPath("//span[text()=' شكراً لكم! ']");

        public static By ProdSuccessMessage = By.XPath("//span[text()=' شكراً لك! ']");
        public static By ParticularEntity = By.XPath("(//mat-option/span[@class='mat-option-text'])[2]");
    }
}
