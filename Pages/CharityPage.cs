using OpenQA.Selenium;


namespace SD.Pages
{
    class CharityPage
    {
        public static By AwqafOrganization = By.XPath("//input[@value='1']/parent::span/parent::label/span[2]");
        public static By SelectedOrganizationName = By.XPath("(//div[1]/p[contains(@class, 'mainColor') and contains(@class, 'fontFamilyMedium') and contains(@class, 'fontSize14')])[2]");

        public static By ApplyFiltersButton = By.XPath("//button[contains(text(),'تطبيق') or contains(text(),'Apply')]");
        public static By SharjahCharityOrganization = By.XPath("//input[@value='2']/parent::span/parent::label/span[2]");
        public static By SharjahHumanitarianOrganization = By.XPath("//input[@value='3']/parent::span/parent::label/span[2]");

        public static By FiveDirhemDonation = By.XPath("(//span[contains(text(),'5 د.إ')])[1]");
        public static By FirstDonation = By.XPath("(//div/button[contains(@class, 'btn p-0')])[1]");
        public static By DonateNowButton = By.XPath("(//div/button[contains(@class, 'btnLightGreen')])[1]");
        public static By ConfirmDonationAmountButton = By.XPath("//button[contains(text(), 'دفع رسوم الخدمة')]");

        public static By PaymentSuccessPage = By.XPath("//h5[@class='mainColor text-center' and text()='تم تنفيذ طلبك بنجاح']");
        public static By ConfirmationMessage = By.XPath("//div/div/p[contains(text(), 'تم إتمام عملية التبرع الخاصة بك')]");

    }
}
