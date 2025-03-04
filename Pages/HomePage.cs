using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class HomePage
    {
        //public static By UserIcon = By.XPath("//button[contains(@class,'btnGeneral ')]/span[text()='account_circle']");
        public static By UserIcon = By.XPath("//div/span[text()='menu']");
        public static By UserActivity = By.XPath("(//div[@class='menu-item-content']/p[contains(text(),'Activities') or contains(text(),'الأنشطة')])[2]");
        public static By LogoutButton = By.XPath("//p[contains(text(),'تسجيل الخروج') or contains(text(),'Logout')]");
        public static By UserName = By.XPath("//section/div/p[contains(@class,'fontFamilyRegular-New')]");
        public static By SurveyIcon = By.XPath("//a/span[contains(text(),'Submit Survey') or contains(text(),'بدء الاستبيان')]");
        public static By TransportCategory = By.XPath("//img[@alt='Transport']");
        public static By CharityCategory = By.XPath("//img[@alt='Charity']");

        public static By GeneralCategory = By.XPath("//div[@class='services-card-content']/div[contains(text(),'General') or contains(text(),'عام')]");
        public static By SearchInput = By.XPath("//input[@placeholder='ما الخدمة التي تبحث عنها؟']");
        public static By SearchFirstResult = By.XPath("(//p[contains(@class,'breakWord')])[1]");

        public static By FavouriteServicesLabel = By.XPath("//h3[contains(text(), 'الخدمات المفضلة')]");
        public static By FirstFavouriteServiceName = By.XPath("//h3[contains(text(), 'الخدمات المفضلة')]/following-sibling::div[contains(@class, 'categorySlider')]//p");

        public static By DashboardLink = By.XPath("//span[contains(text(), 'لوحة البيانات')]");

        public static By SupportLink = By.XPath("//li/a[contains(text(),'Support') or contains(text(),'مركز الدعم')]");

        public static By TrasportCategoryNew = By.XPath("//div[@class='services-card-content']/div[contains(text(),'Transport') or contains(text(),'المواصلات')]");

    }
}
