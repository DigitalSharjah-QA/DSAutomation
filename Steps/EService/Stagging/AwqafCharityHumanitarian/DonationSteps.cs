using TechTalk.SpecFlow;
using SD.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SD.Pages;
using SD.Helpers.Enums;
using System.Threading;
using NUnit.Framework;
using SD.Datasources.Attachments;


namespace SD.Steps.Eservice.Stagging
	{
		[Binding]
		public class DonationSteps
		{

 private readonly ScenarioContext _scenarioContext;
    public ApiHelper apiHelper;
    private readonly IWebDriver _webDriver;
    private readonly WebDriverWait _webDriverWait;
     
    public string AmountToPay;

    public DonationSteps(ScenarioContext scenarioContext) {
      _scenarioContext = scenarioContext;
      _webDriver = DriverManager.WebDriver;
      _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);

    }



[Then(@"User in Donation Service Catelog Page")]
public void ThenUserinFatwaServiceCatelogPage()
{
            EnvironmentHelper.NavigateAndAssertUrl(_webDriver,PageUrls.DonationCatalogPage);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationCatalogPage);
            Thread.Sleep(2000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationCatalogPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            IWebElement serviceNameLabel = _webDriver.FindElement(ServiceCatalogPage.ServiceName);

            Assert.AreEqual("تبرع", serviceNameLabel.Text.ToString());

            Assert.AreEqual("خدمة فورية.", _webDriver.FindElement(ServiceCatalogPage.ProcedureTime).Text.ToString());

            Assert.AreEqual("مواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString());
            Assert.AreEqual("مقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString());
            Assert.AreEqual("زائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString());

Assert.AreEqual("مبلغ التبرع", _webDriver.FindElement(FatwaPage.Fee).Text.ToString());
            Assert.AreEqual(1, _webDriver.FindElements(FatwaPage.StepsCount).Count);
            Assert.AreEqual("عملية الدفع", _webDriver.FindElement(FatwaPage.StepOne).Text.ToString());

            _webDriver.FindElement(ServiceCatalogPage.EntityDetailsTab).Click();
          WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.EntitesList, ElementState.VISIBLE);

            Assert.AreEqual(3, _webDriver.FindElements(ServiceCatalogPage.EntitesList).Count);
            Assert.AreEqual("دائرة الأوقاف بالشارقة", _webDriver.FindElement(ServiceCatalogPage.firstEntity).Text.ToString());
            Assert.AreEqual("جمعية الشارقة الخيرية", _webDriver.FindElement(ServiceCatalogPage.secondEntity).Text.ToString());
            Assert.AreEqual("مدينـــة الشارقة للخدمات الإنسانية", _webDriver.FindElement(ServiceCatalogPage.ThirdEntity).Text.ToString());
            EnvironmentHelper.ScrollTillFind(_webDriver,ServiceCatalogPage.StartServiceButton);
            _webDriver.FindElement(ServiceCatalogPage.StartServiceButton).Click();
}

 
[Then(@"User Submit Donation")]
public void ThenUserSubmitforFatwa()
{
  //donation for awqaf
            Thread.Sleep(3000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServicePage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.FiveDirhemDonation, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.AwqafOrganization, ElementState.VISIBLE);
            Assert.AreEqual("دائرة الأوقاف",_webDriver.FindElement(CharityPage.AwqafOrganization).Text.ToString());
            _webDriver.FindElement(CharityPage.AwqafOrganization).Click();
            EnvironmentHelper.ScrollTillFind(_webDriver,CharityPage.ApplyFiltersButton);
             _webDriver.FindElement(CharityPage.ApplyFiltersButton).Click();
             Thread.Sleep(999);
             EnvironmentHelper.ScrollTillFind(_webDriver,CharityPage.SelectedOrganizationName);
            Assert.AreEqual("دائرة الأوقاف",_webDriver.FindElement(CharityPage.SelectedOrganizationName).Text.ToString());
            EnvironmentHelper.ScrollTillFind(_webDriver,CharityPage.FirstDonation);
            _webDriver.FindElement(CharityPage.FirstDonation).Click();
             WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.DonateNowButton, ElementState.VISIBLE);
              EnvironmentHelper.ScrollTillFind(_webDriver,CharityPage.DonateNowButton);
              Thread.Sleep(3000);
            _webDriver.FindElement(CharityPage.DonateNowButton).Click();
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmDonationAmountButton, ElementState.VISIBLE);
                Thread.Sleep(2000);
            _webDriver.FindElement(CharityPage.ConfirmDonationAmountButton).Click();
              DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.DonationPaymentPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.FirstPaymentMethods, ElementState.VISIBLE);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.VisaPaymentMethod, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.VisaPaymentMethod).Click();
           Assert.AreEqual("5.00 د.إ.",_webDriver.FindElement(PaymentPage.TotalAmountToPay).Text.ToString());
           _webDriver.FindElement(PaymentPage.PaymentContineButton).Click();
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.PaymentPayByButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.PaymentPayByButton).Click();
           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.PaymentSimulationPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.ACSPaymentSubmitButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.ACSPaymentSubmitButton).Click();  
           Thread.Sleep(2000);

            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServiceConfirmationPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmationMessage, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(4));
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.PaidAmount, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.ViewOrDownloadReceipt, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.DonateAgain, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);
           
}



[Then(@"User donate Zakat for Sharjah Charity International")]
public void ThenUserdonateZakatforSharjahCharityInternational()
{
            Thread.Sleep(3000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServicePage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.FiveDirhemDonation, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.SharjahCharityOrganization, ElementState.VISIBLE);
            Assert.AreEqual("جمعية الشارقة الخيرية",_webDriver.FindElement(CharityPage.SharjahCharityOrganization).Text.ToString());
             _webDriver.FindElement(CharityPage.SharjahCharityOrganization).Click();
             EnvironmentHelper.ScrollTillFind(_webDriver, CharityPage.ApplyFiltersButton);
             _webDriver.FindElement(CharityPage.ApplyFiltersButton).Click();
             Thread.Sleep(999);
            Assert.AreEqual("جمعية الشارقة الخيرية",_webDriver.FindElement(CharityPage.SelectedOrganizationName).Text.ToString());
            EnvironmentHelper.ScrollTillFind(_webDriver, CharityPage.FirstDonation);
            _webDriver.FindElement(CharityPage.FirstDonation).Click();
            Thread.Sleep(1000);
             WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.DonateNowButton, ElementState.VISIBLE);
               Thread.Sleep(3000);
            _webDriver.FindElement(CharityPage.DonateNowButton).Click();
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmDonationAmountButton, ElementState.VISIBLE);
                Thread.Sleep(2000);
            _webDriver.FindElement(CharityPage.ConfirmDonationAmountButton).Click();
              DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.DonationPaymentPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.FirstPaymentMethods, ElementState.VISIBLE);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.VisaPaymentMethod, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.VisaPaymentMethod).Click();
           Assert.AreEqual("5.00 د.إ.",_webDriver.FindElement(PaymentPage.TotalAmountToPay).Text.ToString());
           _webDriver.FindElement(PaymentPage.PaymentContineButton).Click();
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.PaymentPayByButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.PaymentPayByButton).Click();
           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.PaymentSimulationPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.ACSPaymentSubmitButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.ACSPaymentSubmitButton).Click();  
           Thread.Sleep(2000);
                       DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServiceConfirmationPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmationMessage, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(4));
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.PaidAmount, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.ViewOrDownloadReceipt, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.DonateAgain, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);
}



[Then(@"User Submit Donation for Sharjah Humanitarian Services")]
public void ThenUserSubmitDonationforSharjahHumanitarianServices()
{
	            Thread.Sleep(3000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServicePage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.FiveDirhemDonation, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.SharjahHumanitarianOrganization, ElementState.VISIBLE);
            Assert.AreEqual("مدينـــة الشارقة للخدمات الإنسانية",_webDriver.FindElement(CharityPage.SharjahHumanitarianOrganization).Text.ToString());
             _webDriver.FindElement(CharityPage.SharjahHumanitarianOrganization).Click();
             EnvironmentHelper.ScrollTillFind(_webDriver, CharityPage.ApplyFiltersButton);
             _webDriver.FindElement(CharityPage.ApplyFiltersButton).Click();
             Thread.Sleep(999);
            Assert.AreEqual("مدينـــة الشارقة للخدمات الإنسانية",_webDriver.FindElement(CharityPage.SelectedOrganizationName).Text.ToString());
            EnvironmentHelper.ScrollTillFind(_webDriver, CharityPage.FirstDonation);
            _webDriver.FindElement(CharityPage.FirstDonation).Click();
             Thread.Sleep(1500);
             WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.DonateNowButton, ElementState.VISIBLE);
             EnvironmentHelper.ScrollTillFind(_webDriver, CharityPage.DonateNowButton);
            _webDriver.FindElement(CharityPage.DonateNowButton).Click();
            Thread.Sleep(1000);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmDonationAmountButton, ElementState.VISIBLE);
            Thread.Sleep(1500);
            _webDriver.FindElement(CharityPage.ConfirmDonationAmountButton).Click();
           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.DonationPaymentPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.FirstPaymentMethods, ElementState.VISIBLE);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.VisaPaymentMethod, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.VisaPaymentMethod).Click();
           Assert.AreEqual("5.00 د.إ.",_webDriver.FindElement(PaymentPage.TotalAmountToPay).Text.ToString());
           _webDriver.FindElement(PaymentPage.PaymentContineButton).Click();
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.PaymentPayByButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.PaymentPayByButton).Click();
           DriverExtensions.WaitUntilPageLoaded(_webDriver,PageUrls.PaymentSimulationPage);
           WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.ACSPaymentSubmitButton, ElementState.VISIBLE);
           _webDriver.FindElement(PaymentPage.ACSPaymentSubmitButton).Click();  
           Thread.Sleep(2000);
                                  DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.DonationServiceConfirmationPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, CharityPage.ConfirmationMessage, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count,Is.EqualTo(4));
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.PaidAmount, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.ViewOrDownloadReceipt, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.DonateAgain, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver,_webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);
}

		}

    
        }