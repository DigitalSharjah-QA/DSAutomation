using OpenQA.Selenium;

namespace SD.Pages
{
    class WeatherPage
    {
        public static By TodayWeatherWidget = By.XPath("//div[@class='row align-items-center']/div[1]/p[contains(text(), 'اليوم')]");
        public static By SixDaysWidgets = By.XPath("//div[@class='row align-items-center']/div[1]/p");

        public static By SixDates = By.XPath("//div[@class='row align-items-center']/div[2]/p");

        public static By CurrentTempValue = By.XPath("//p[contains(@class,'fontcurrentTemp')]");

    }
}
