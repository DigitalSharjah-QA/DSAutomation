using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class LoginPage
    {
        //public static By UsersList = By.XPath("//div[@id='custom-dropdown']");
        public static By UsersList = By.XPath("//div/mat-select[contains(@id,'mat-select')]");
        public static By UAEPassButton = By.XPath("//img[@alt='UAE Pass Icon']");

    }
}
