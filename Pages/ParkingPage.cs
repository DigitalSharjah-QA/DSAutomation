using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
    class ParkingPage
    {
    
                public static By PaymentSuccessPage = By.XPath("//h5[@class='mainColor text-center' and text()='تم تنفيذ طلبك بنجاح']");
                public static By InProgressMessage = By.XPath("//h5[@class='mainColor text-center' and text()='طلبك قيد التنفيذ']");
                 public static By MunicipilityList = By.XPath("//div[starts-with(@id, 'mat-select-value')]");
                 public static By KhorfkanOption = By.XPath("//span[@class='mat-option-text' and text()=' خورفكان ']");

               public static By ConfirmationMessage = By.XPath("//div/div/p[contains(text(), 'تم إتمام عملية الدفع الخاصة بك.')]");

    }
}
