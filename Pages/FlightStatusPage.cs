using OpenQA.Selenium;


namespace SD.Pages
{
    class FlightStatusPage
    {
        public static By UpperSection = By.XPath("(//div[contains(@class, 'flightShadowCont')])[1]");
        public static By dayNameInArabic = By.XPath("(//div/div/p/span)[1]");
        public static By FlighCard = By.XPath("(//div[@class='ng-star-inserted']/following-sibling::div[@class='ng-star-inserted'])[5]");
        public static By YesterdayFilterButton = By.XPath("(//div/button[contains(text(), 'الأمس')])[1]");

        public static By tomorrowFilterButton = By.XPath("//button[contains(text(), ' غدا ')]");

        public static By FirstFlightCompanyName = By.XPath("(//div/div/div/div/div/div/div/div/p)[3]");
        public static By FirstFlightCode = By.XPath("(//div/p[contains(@class, 'flightCodeColor') and not(contains(text(), 'حزام'))])[1]");

        public static By SearchInput = By.XPath("//input[contains(@class, 'mat-input-element') and contains(@id, 'mat-input')]");

    }
}
