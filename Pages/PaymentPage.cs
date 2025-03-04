using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class PaymentPage
    {
        public static By VisaPaymentMethod = By.XPath("//span[contains(text(), ' 4242•••• ')]");

        public static By TotalAmountToPay = By.XPath("//p[@class='gray900Color fontFamilyRegular-New fontSize24 text-end mb-0']");

                public static By PaymentContineButton = By.XPath("//button[contains(text(), ' متابعة ')]");
                public static By PaymentPayByButton = By.XPath("//button[contains(@class, 'btnGeneral') and contains(text(), 'الدفع عن طريق')]");
                public static By CardNumberInputInTahseelPage = By.XPath("//input[@type='tel' and @formcontrolname='cardNumber']");

                public static By ACSPaymentSubmitButton = By.XPath("//input[@type='submit' and @value='Submit']");

                public static By PaymentMethods = By.XPath("//*[@aria-label='Select an option']/div/mat-radio-button/label/span[2]/div/div[2]/span");
                public static By FirstPaymentMethods = By.XPath("(//img[@alt='payment method icon'])[2]");

                public static By TahseelPaymentMethods = By.XPath("//span[contains(text(), 'تحصيل')]/parent::div/parent::div/div[1]/img[@alt='payment method icon']");

    }
}
