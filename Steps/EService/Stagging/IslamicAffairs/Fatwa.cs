using TechTalk.SpecFlow;
using SD.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SD.Pages;
using SD.Helpers.Enums;
using System.Threading;
using NUnit.Framework;
using SD.Datasources.Attachments;
using System.Text.RegularExpressions;


namespace SD.Steps.Eservice.Stagging
	{
		[Binding]
		public class Fatwa
		{

 private readonly ScenarioContext _scenarioContext;
    public ApiHelper apiHelper;
    private readonly IWebDriver _webDriver;
    private readonly WebDriverWait _webDriverWait;
     
    public string AmountToPay;

    public Fatwa(ScenarioContext scenarioContext) {
      _scenarioContext = scenarioContext;
      _webDriver = DriverManager.WebDriver;
      _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);

    }


[Then(@"User Search for the Fatwa Service And Select it")]
public void ThenUserSearchfortheFatwaServiceAndSelectit()
{
EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.FatwaServicePage);
}


[Then(@"User in Fatwa Service Catelog Page")]
public void ThenUserinFatwaServiceCatelogPage()
{
	        DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.FatwaServicePage);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            IWebElement serviceNameLabel = _webDriver.FindElement(ServiceCatalogPage.ServiceName);
            Thread.Sleep(1000);
            Assert.AreEqual("فتوى", serviceNameLabel.Text.ToString());

            Assert.AreEqual("حسب الطلب", _webDriver.FindElement(ServiceCatalogPage.ProcedureTime).Text.ToString());

      if (EnvironmentHelper.CurrentStage == Stage.SD_Stagging)
      {
        Assert.AreEqual("مواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString().Trim());
        Assert.AreEqual("مقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString().Trim());
        Assert.AreEqual("زائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString().Trim());
      }
      else
      {
        Assert.AreEqual("الزائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString().Trim());
        Assert.AreEqual("المقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString().Trim());
        Assert.AreEqual("المواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString().Trim());
      }
      

            Assert.AreEqual("مجانا", _webDriver.FindElement(FatwaPage.Fee).Text.ToString());
            
            Assert.AreEqual(3, _webDriver.FindElements(FatwaPage.StepsCount).Count);
            Assert.AreEqual("التقديم على الطلب", _webDriver.FindElement(FatwaPage.StepOne).Text.ToString());
            Assert.AreEqual("مراجعة الطلب", _webDriver.FindElement(FatwaPage.StepTwo).Text.ToString());
            Assert.AreEqual("إنجاز الخدمة", _webDriver.FindElement(FatwaPage.StepThree).Text.ToString());

          _webDriver.FindElement(ServiceCatalogPage.EntityDetailsTab).Click();
          WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.EntitesList, ElementState.VISIBLE);

            Assert.AreEqual(1, _webDriver.FindElements(ServiceCatalogPage.EntitesList).Count);
            Assert.AreEqual("دائرة الشؤون الإسلامية بالشارقة", _webDriver.FindElement(ServiceCatalogPage.firstEntity).Text.ToString().Trim());

          EnvironmentHelper.ScrollTillFind(_webDriver,ServiceCatalogPage.StartServiceButton);            
          _webDriver.FindElement(ServiceCatalogPage.StartServiceButton).Click() ;
}


[Then(@"User Submit for Fatwa")]
public void ThenUserSubmitforFatwa()
{
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.FatwaServicePage);
            Thread.Sleep(3000);
            EnvironmentHelper.HandleIfLoginNotComplete(_webDriver);
            Assert.AreEqual(EnvironmentHelper._currentLoggedInUser.Mobile.ToString(),_webDriver.FindElement(FatwaPage.FatwaFormMobileNumberInput).Text.ToString().Trim());
            IWebElement FatwaText=  _webDriver.FindElement(FatwaPage.FatwaInput);
           EnvironmentHelper.EnterTextLetterByLetter(FatwaText,"Fatwa Test Please Ignore");  
           
            IWebElement fileInput = _webDriver.FindElement(By.Id("getFile3"));
             fileInput.SendKeys(AttachmentsPaths.GetPath("attachment.jpg"));
            WaitUtils.elementState(_webDriver,_webDriverWait, FatwaPage.UplaodedAttachemntName, ElementState.VISIBLE);
            Assert.AreEqual("attachment.jpg",_webDriver.FindElement(FatwaPage.UplaodedAttachemntName).Text);
            EnvironmentHelper.ScrollTillFind(_webDriver,FatwaPage.FatwaSubmitbutton);
            _webDriver.FindElement(FatwaPage.FatwaSubmitbutton).Click();
            
                      if( _webDriver.FindElements(EnvironmentHelper.dialogLocator).Count >0){
                        _webDriver.FindElement(EnvironmentHelper.dialogLocator).Click();
                        }
            Thread.Sleep(4000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.FatwaServiceConfirmationPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, FatwaPage.ConfirmationMessage, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(4));
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.ReferenceNumber, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.TrackYourRequest, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.SubmitAnotherRequest, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);
            StringAssert.IsMatch(@"^\d{5,6}$", Regex.Replace(_webDriver.FindElement(ConfirmationPage.ReferenceNumber).Text.ToString(), @"\D", ""));
 
}


		}

    internal class JSONObject
    {
    }
}