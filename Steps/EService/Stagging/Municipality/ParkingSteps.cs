using TechTalk.SpecFlow;
using SD.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SD.Pages;
using SD.Helpers.Enums;
using System;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;
using SD.Datasources.Users;
using System.Text.RegularExpressions;
using SD.Datasources.Attachments;
using OpenQA.Selenium.Interactions;
using SD.Steps.Helpers;
using FluentAssertions;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.Steps.Eservice.Stagging
{
    [Binding]
    public class ParkingSteps
    {

        private readonly ScenarioContext _scenarioContext;
        public ApiHelper apiHelper;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;

        public string AmountToPay;

        public ParkingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
        }


        [Given(@"User click on Transportation Category")]
        public void GivenUserclickonTransportationCategory()
        {

            EnvironmentHelper.ScrollTillFind(_webDriver, HomePage.TrasportCategoryNew);
            WaitUtils.elementState(_webDriver, _webDriverWait, HomePage.TrasportCategoryNew, ElementState.VISIBLE);
            IWebElement TransportCategory = EnvironmentHelper.ScrollTillFind(_webDriver, HomePage.TrasportCategoryNew);
            TransportCategory.Click();
        }


        [Then(@"User in Service (.*) Catelog Page")]
        public void ThenUserInServiceCatelogPage(string p0)
        {
            try
            {
                DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.ParkingCatalogPage);
                WaitUtils.elementState(_webDriver, _webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
                IWebElement serviceNameLabel = _webDriver.FindElement(ServiceCatalogPage.ServiceName);
                Assert.AreEqual(p0, serviceNameLabel.Text.ToString());
                Assert.AreEqual("عملية الدفع", _webDriver.FindElement(ServiceCatalogPage.StepOne).Text.ToString());
                Assert.AreEqual("خدمة فورية", _webDriver.FindElement(ServiceCatalogPage.ProcedureTime).Text.ToString());
                Assert.AreEqual("مواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString());
                Assert.AreEqual("مقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString());
                Assert.AreEqual("زائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString());
                Assert.AreEqual(2, _webDriver.FindElements(ServiceCatalogPage.EntitesList).Count);
                Assert.AreEqual("بلدية مدينة الشارقة", _webDriver.FindElement(ServiceCatalogPage.firstEntity).Text.ToString());
                Assert.AreEqual("بلدية مدينة خورفكان", _webDriver.FindElement(ServiceCatalogPage.secondEntity).Text.ToString());
                Assert.AreEqual(3, _webDriver.FindElements(ServiceCatalogPage.PaymentMethods).Count);
                Assert.AreEqual("حساب تحصيل", _webDriver.FindElement(ServiceCatalogPage.firstPaymentMethod).Text.ToString());
                Assert.AreEqual("بطاقة ائتمان", _webDriver.FindElement(ServiceCatalogPage.secondPaymentMethod).Text.ToString());
                Assert.AreEqual("تحويل بنكي", _webDriver.FindElement(ServiceCatalogPage.thirdPaymentMethod).Text.ToString());
                Assert.AreEqual(7, _webDriver.FindElements(ServiceCatalogPage.FeesList).Count);
                ReadOnlyCollection<IWebElement> elements = _webDriver.FindElements(ServiceCatalogPage.FeesValues);
                List<string> actualValues = elements.Select(element => element.Text.Trim()).ToList();
                List<string> expectedValues = new List<string> { "2 درهم", "5 درهم", "8 درهم", "12 درهم" };
                Assert.AreEqual(expectedValues.Count, elements.Count);
                CollectionAssert.AreEqual(expectedValues, actualValues);
            }
            catch
            {
                Assert.AreEqual(p0, _webDriver.FindElement(ServiceCatalogPage.ParkingName).Text.ToString());
            }
        }

        [Then(@"User Start the Service (.*)")]
        public void ThenUserStartTheService(string p0)
        {
            try
            {
                _webDriver.FindElement(ServiceCatalogPage.StartServiceButton).Click();
            }
            //Catalogue Page does not exist-Configured from BackEnd
            catch
            {
                Thread.Sleep(1000);
            }
        }


        [Then(@"User Pay for The First Vehicle")]
        public void ThenUserPayForTheFirstVehicle()
        {
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.VehiclesListPage);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.FirstVehicleNumber, ElementState.VISIBLE);
            Thread.Sleep(999);
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.AddNewVehicleButton);
            _webDriver.FindElement(VehiclesListPage.AddNewVehicleButton).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.PlateNumberInput, ElementState.VISIBLE);

            Random random = new Random();
            // Generate a random number
            int randomNumber = random.Next(1000, 10000); // Adjust the range as needed
            // Your original XPath with the random number
            string xpath = $"//p[text()=' {randomNumber} ']/ancestor::div[3]/following-sibling::div/p/i";
            string xpathNew = "((//div[contains(@class,'end')]/p[contains(text(),'" + randomNumber + "')])[1]//following::div/button)[1]";
            _webDriver.FindElement(VehiclesListPage.PlateNumberInput).SendKeys(randomNumber.ToString());
            _webDriver.FindElement(VehiclesListPage.SourceList).Click();
            Thread.Sleep(1000);
            try
            {
                _webDriver.FindElement(VehiclesListPage.SharjahOption).Click();

            }
            catch
            {
                _webDriver.FindElement(VehiclesListPage.SourceList).Click();
                Thread.Sleep(1000);
                _webDriver.FindElement(VehiclesListPage.SharjahOption).Click();
            }
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.TypeList, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.TypeList);
            _webDriver.FindElement(VehiclesListPage.TypeList).Click();
            Thread.Sleep(1000);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.CommercialOption, ElementState.VISIBLE);
            _webDriver.FindElement(VehiclesListPage.CommercialOption).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.CodeList, ElementState.VISIBLE);
            _webDriver.FindElement(VehiclesListPage.CodeList).Click();
            Thread.Sleep(1000);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.CodeOption, ElementState.VISIBLE);
            _webDriver.FindElement(VehiclesListPage.CodeOption).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.SubmitAddVehicleButton, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.SubmitAddVehicleButton);
            _webDriver.FindElement(VehiclesListPage.SubmitAddVehicleButton).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.ManageVehiclesButton, ElementState.VISIBLE);
            Thread.Sleep(1000);
            _webDriver.FindElement(VehiclesListPage.ManageVehiclesButton).Click();
            Thread.Sleep(2000);

            IWebElement foundElement = EnvironmentHelper.ScrollTillFind(_webDriver, By.XPath(xpathNew));
            foundElement.Click();

            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.DeleteVehiclesButton, ElementState.VISIBLE);
            Thread.Sleep(500);
            _webDriver.FindElement(VehiclesListPage.DeleteVehiclesButton).Click();
            Thread.Sleep(500);
            WaitUtils.elementState(_webDriver, _webDriverWait, By.XPath($"//p[text()='{randomNumber} ']"), ElementState.NOT_VISIBLE);
            //////
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_webDriver;
            jsExecutor.ExecuteScript("window.scrollTo(0, 0);");
            Thread.Sleep(500);
            jsExecutor.ExecuteScript("window.scrollTo(0, 0);");
            Thread.Sleep(500);
            ////// 

            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.DoneButton, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.DoneButton);
            _webDriver.FindElement(VehiclesListPage.DoneButton).Click();
            Thread.Sleep(2000);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.FirstVehicleNumber, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.FirstVehicleNumber);
            _webDriver.FindElement(VehiclesListPage.FirstVehicleNumber).Click();
            Thread.Sleep(500);
            if (_webDriver.FindElements(VehiclesListPage.AlreadyBookedMsg).Count > 0)
            {
                Thread.Sleep(6600);
                _webDriver.FindElement(VehiclesListPage.SecondVehicleNumber).Click();
                if (_webDriver.FindElements(VehiclesListPage.AlreadyBookedMsg).Count > 0)
                {
                    Thread.Sleep(6600);
                    _webDriver.FindElement(VehiclesListPage.ThirdVehicleNumber).Click();
                }
            }

        }





        [Then(@"user click Next for (.*) hour")]
        public void ThenuserclickNextforhour(int args1)
        {
            Thread.Sleep(2000);
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.AmountToPay, ElementState.VISIBLE);
            AmountToPay = _webDriver.FindElement(VehiclesListPage.AmountToPay).Text.ToString();
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.ParkingDurationNextButton);
            _webDriver.FindElement(VehiclesListPage.ParkingDurationNextButton).Click();
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);

        }


        [Then(@"user click Next for (.*) hour for Khorfacan Municipality")]
        public void ThenuserclickNextforhourForKhorfacan(int args1)
        {
            Thread.Sleep(2000);
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.AmountToPay, ElementState.VISIBLE);
            Thread.Sleep(500);
            _webDriver.FindElement(ParkingPage.MunicipilityList).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, ParkingPage.KhorfkanOption, ElementState.VISIBLE);
            Thread.Sleep(999);
            _webDriver.FindElement(ParkingPage.KhorfkanOption).Click();
            Thread.Sleep(999);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.AmountToPay, ElementState.VISIBLE);
            AmountToPay = _webDriver.FindElement(VehiclesListPage.AmountToPay).Text.ToString();
            EnvironmentHelper.ScrollTillFind(_webDriver, VehiclesListPage.ParkingDurationNextButton);
            _webDriver.FindElement(VehiclesListPage.ParkingDurationNextButton).Click();
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
        }



        [When(@"User Select Payment Details in Payment Page")]
        public void WhenUserSelectPaymentDetailsinPaymentPage()
        {
            Thread.Sleep(999);
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.ParkingPaymentPage);
            EnvironmentHelper.ClickTryAgainIfAppear(_webDriver);
            WaitUtils.elementState(_webDriver, _webDriverWait, PaymentPage.FirstPaymentMethods, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, PaymentPage.VisaPaymentMethod, ElementState.VISIBLE);
            _webDriver.FindElement(PaymentPage.VisaPaymentMethod).Click();
            //Assert.AreEqual(AmountToPay,_webDriver.FindElement(PaymentPage.TotalAmountToPay).Text.ToString());

            WaitUtils.elementState(_webDriver, _webDriverWait, PaymentPage.PaymentContineButton, ElementState.VISIBLE);
            EnvironmentHelper.ScrollTillFind(_webDriver, PaymentPage.PaymentContineButton);

            _webDriver.FindElement(PaymentPage.PaymentContineButton).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, PaymentPage.PaymentPayByButton, ElementState.VISIBLE);
            _webDriver.FindElement(PaymentPage.PaymentPayByButton).Click();
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.PaymentSimulationPage);
            WaitUtils.elementState(_webDriver, _webDriverWait, PaymentPage.ACSPaymentSubmitButton, ElementState.VISIBLE);
            _webDriver.FindElement(PaymentPage.ACSPaymentSubmitButton).Click();
            //    WaitUtils.elementState(_webDriver,_webDriverWait, ParkingPage.InProgressMessage, ElementState.VISIBLE);


            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.ParkingServiceConfirmationPage);
            WaitUtils.elementState(_webDriver, _webDriverWait, ParkingPage.ConfirmationMessage, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.GreenConfirmationIcon, ElementState.VISIBLE);
            Assert.That(_webDriver.FindElements(ConfirmationPage.OptionsListAppearInConfirmationPage).Count, Is.EqualTo(6));
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.PaidAmount, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.TrackYourRequest, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.ViewOrDownloadReceipt, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.PayAgain, ElementState.VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, ConfirmationPage.Rating, ElementState.VISIBLE);

        }


        [Then(@"User should get a Parking notification")]
        public void ThenUsershouldgetaParkingnotification()
        {



        }

    }
}