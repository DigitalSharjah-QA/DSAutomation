using OpenQA.Selenium;


namespace SD.Pages
{
    class ConfirmationPage
    {
        public static By GreenConfirmationIcon = By.CssSelector("div > svg > g > g:nth-child(1) > g > path[stroke='rgb(0,97,88)']");

        public static By OptionsListAppearInConfirmationPage=By.XPath("//div[contains(@class, 'listGroupItem')]");
        public static By SubmitAnotherSurvey=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'تقديم إستبيان آخر')]");
        public static By SubmitAnotherRequest=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), ' تقديم طلب جديد ')]");
        public static By ReferenceNumber=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'الرقم المرجعي')]");
        public static By TrackYourRequest=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'تتبع حالة الطلب')]");
        public static By Rating=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'كيف كانت تجربتك؟')]");

        public static By PaidAmount=By.XPath("//div[contains(@class, 'listGroupItem')]/p[contains(text(), 'المبلغ المدفوع')]");

        public static By  ViewOrDownloadReceipt =By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), ' عرض أو تحميل إيصال الدفع')]");

         public static By DonateAgain=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'تبرع مرة أخرى')]");

         public static By PayAgain=By.XPath("//div[contains(@class, 'listGroupItem')]/div/button/span[contains(text(), 'إجراء عملية دفع أخرى')]");


    }
}
