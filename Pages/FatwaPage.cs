using OpenQA.Selenium;


namespace SD.Pages
{
    class FatwaPage
    {
        public static By Fee = By.XPath("((//div/p[contains(text(),'Fees') or contains(text(),'رسوم الخدمة')])[1]//following::li/div/span)[1]");

        public static By StepsCount = By.XPath("(//div/p[contains(text(),'Steps') or contains(text(),'خطوات التقديم على الخدمة')])[1]//following::div[@class='node position-relative mb-2 centerFlex']/p");

        public static By StepOne = By.XPath("((//div/p[contains(text(),'Steps') or contains(text(),'خطوات التقديم على الخدمة')])[1]//following::div[@class='node position-relative mb-2 centerFlex']/p)[1]");
        public static By StepTwo = By.XPath("((//div/p[contains(text(),'Steps') or contains(text(),'خطوات التقديم على الخدمة')])[2]//following::div[@class='node position-relative mb-2 centerFlex']/p)[2]");
        public static By StepThree = By.XPath("((//div/p[contains(text(),'Steps') or contains(text(),'خطوات التقديم على الخدمة')])[2]//following::div[@class='node position-relative mb-2 centerFlex']/p)[3]");
        public static By FatwaFormMobileNumberInput = By.XPath("//p[contains(text(), 'رقم الهاتف المتحرك') or contains(text(),'Mobile Number')]/parent::div/following-sibling::div/p");
        public static By FatwaFormAttachmentInput = By.Id("getFile3");
        public static By UplaodedAttachemntName = By.XPath("//div/a/span");
        public static By FatwaInput = By.XPath("//textarea[contains(@data-placeholder,'إدخل نص سؤالك') or contains(@data-placeholder,'Enter your question')]");
        public static By FatwaSubmitbutton = By.XPath("//button[contains(@type,'button') and (contains(text(),'Submit') or contains(text(),'إرسال'))]");
        public static By ConfirmationMessage = By.XPath("//span[contains(text(), ' تم تقديم طلبك بنجاح ')]");
         public static By ReferenceNumber = By.XPath("(//div/div/div/p)[5]");
         public static By ReferenceNumberValue = By.XPath("(//div/div/div/div[2]/div/h5)[1]");


    }
}
