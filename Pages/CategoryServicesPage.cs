using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class CategoryServicesPage
    {
        public static By SearchInput = By.XPath("//input[contains(@class, 'search-input')]");

        public static By FirstSearchResult = By.XPath("//p[@class='fontFamilyMedium mb-0 font-weight-bold']");

        public static By SearchServiceNew = By.XPath("//div/input[@id='mat-input-0']");
        public static By FirstSearchResultNew = By.XPath ("//div[contains(@class,'row justify-content-center')]//div[@class='d-flex align-items-center']");

    }
}
