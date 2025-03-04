using OpenQA.Selenium;

namespace SD.Pages
{
    class RoadsAndTransportPage
    {
        public static By LocationTextInput = By.XPath("//input[@placeholder='البحث عن الموقع']");
        public static By FirstSearchResult = By.XPath("(//span[@class='mat-option-text'])[1]");
        public static By NextButton = By.XPath("//button[text()='التالي']");
        public static By MainCategoryList = By.XPath("//span[contains(@class, 'mat-select-placeholder')]");
        public static By FourthOptionInMainCategoriesList = By.XPath("(//mat-option[@role='option'])[4]");
        public static By DetailsTextField = By.XPath("(//textarea[@aria-required='true'])");
        public static By SubmitButton = By.XPath("//button[text()='إرسال']");

        public static By ConfirmationMessage = By.XPath("//h5[contains(text(), 'سيتم التواصل معك عن طريق الرسائل النصية والبريد الإلكتروني في حال تحديث حالة طلبك.')]");
        public static By ToastErrorMessage = By.XPath("//div[@id='toast-container']/div/div");


    }
}
  