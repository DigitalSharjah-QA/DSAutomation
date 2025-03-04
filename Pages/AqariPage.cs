using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class AqariPage
    {
        public static By AqariIcon = By.XPath("(//div[@id='platformsCards']//div[contains(text(),'عقاري') or contains(text(),'Aqari')])[1]");
        public static By AqariHome = By.XPath("//div[contains(@class,'MuiBox-root')]/a[text()='Home' or text()='الصفحة الرئيسية']");
        public static By ActivitySearchbox = By.XPath("//div[contains(@class,'mat-form-field-infix')]/input");
        public static By ActivityAqariItem = By.XPath("(//app-services-list[@class='ng-star-inserted']//div[@class='ng-star-inserted'])[2]");
    }
}
