using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SD.Pages
{
  class VehiclesListPage
  {
    public static By FirstVehicleNumber = By.XPath("(//div[@class='row w-100 g-4']//button[contains(@class,'btn') and not (contains(@class,'active'))])[1]");
    public static By SecondVehicleNumber = By.XPath("(//div[contains(@class, 'row g-0 w-100 align-items-center pointer mx-2')])[2]");
    public static By ThirdVehicleNumber = By.XPath("(//div[contains(@class, 'row g-0 w-100 align-items-center pointer mx-2')])[3]");
    public static By AlreadyBookedMsg = By.XPath("//div[@aria-label='طلب التمديد غير مسموح به حيث التذكرة السابقة تم حجزها فى خلال اخر 2 دقيقة فقط']");

    public static By ParkingDurationNextButton = By.XPath("//button[contains(@class,'btnGeneral ') and contains(text(),'المتابعة')]");
    public static By AmountToPay = By.XPath("(//div[contains(@class,'row')]/button/div/p[3])[1]");
    public static By VehiclesWithActiveParkings = By.XPath("//i[@class='mat-tooltip-trigger fa fa-parking greenColor mx-1 mx-xl-2 pointer ng-star-inserted']/ancestor::div[3]");
    public static By AddNewVehicleButton = By.XPath("//button[contains(text(), 'إضافة مركبة') or contains(text(),'Add Vehicle')]");
    public static By PlateNumberInput = By.XPath("//input[contains(@id,'mat-input')]");
    public static By SourceList = By.XPath("//div[@id='mat-select-value-1']");
    public static By SharjahOption = By.XPath("//span[@class='mat-option-text' and contains(text(), 'الشارقة')]");

    public static By TypeList = By.XPath("//div[@id='mat-select-value-3']");

    public static By CommercialOption = By.XPath("//span[@class='mat-option-text' and text()=' تجارية']");

    public static By CodeList = By.XPath("//div[@id='mat-select-value-5']");

    public static By CodeOption = By.XPath("(//span[@class='mat-option-text'])[1]");

    public static By SubmitAddVehicleButton = By.XPath("//button[@type='sumbit']");

    public static By ManageVehiclesButton = By.XPath("//button[contains(text(), 'إدارة المركبات')]");

    public static By DeleteVehiclesButton = By.XPath("//button[contains(text(), 'حذف')]");
    public static By DoneButton = By.XPath("//div[contains(@class,'end')]/button[contains(@class, 'col-md-auto')]");

  }
}
