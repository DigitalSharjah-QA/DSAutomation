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

namespace SD.Steps.Eservice.PROD
{
    [Binding]
    public class ParkingSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public ApiHelper apiHelper;
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait,ShorterbDriverWait;

        public string AmountToPay;
        public int triesCount = 0;

        public ParkingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _webDriver = DriverManager.WebDriver;
            _webDriverWait = new WebDriverWait(_webDriver, DriverExtensions._defaultTimeout);
            ShorterbDriverWait=new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
        }

        [Then(@"Production User Pay for The First Vehicle")]
        public void ThenProductionUserPayForTheFirstVehicle()
        {
            
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.VehiclesListPage);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.FirstVehicleNumber, ElementState.VISIBLE);
            _webDriver.FindElement(VehiclesListPage.AddNewVehicleButton).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.PlateNumberInput, ElementState.VISIBLE);
                        Random random = new Random();

            int randomNumber = random.Next(1000, 10000);
            string xpath = $"//p[text()=' {randomNumber} ']/ancestor::div[3]/following-sibling::div/p/i";

            _webDriver.FindElement(VehiclesListPage.PlateNumberInput).SendKeys(randomNumber.ToString());
            _webDriver.FindElement(VehiclesListPage.SourceList).Click();
            Thread.Sleep(1000);
            try{ 
            _webDriver.FindElement(VehiclesListPage.SharjahOption).Click();
           
             }
             catch{
                _webDriver.FindElement(VehiclesListPage.SourceList).Click();
                Thread.Sleep(1000);
                 _webDriver.FindElement(VehiclesListPage.SharjahOption).Click();
             }
              _webDriver.FindElement(VehiclesListPage.TypeList).Click();
            Thread.Sleep(1000);
            _webDriver.FindElement(VehiclesListPage.CommercialOption).Click();
            _webDriver.FindElement(VehiclesListPage.CodeList).Click();
            Thread.Sleep(1000);
            _webDriver.FindElement(VehiclesListPage.CodeOption).Click();
            _webDriver.FindElement(VehiclesListPage.SubmitAddVehicleButton).Click();
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.ManageVehiclesButton, ElementState.VISIBLE);
            _webDriver.FindElement(VehiclesListPage.ManageVehiclesButton).Click();
            Thread.Sleep(1000);
            IWebElement foundElement = EnvironmentHelper.ScrollTillFind(_webDriver, By.XPath(xpath));
            foundElement.Click();

            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.DeleteVehiclesButton, ElementState.VISIBLE);
            Thread.Sleep(500);
            _webDriver.FindElement(VehiclesListPage.DeleteVehiclesButton).Click();
            Thread.Sleep(500);
            WaitUtils.elementState(_webDriver, _webDriverWait,By.XPath($"//p[text()='{randomNumber} ']"), ElementState.NOT_VISIBLE);
            WaitUtils.elementState(_webDriver, _webDriverWait, VehiclesListPage.FirstVehicleNumber, ElementState.VISIBLE);
            Thread.Sleep(1000);
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

        [Then(@"Production User in Service (.*) Catalog Page")]
        public void ThenUserInServiceCatelogPage(string p0)
        {
            Thread.Sleep(1000);
            DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.ParkingCatalogPage);
            WaitUtils.elementState(_webDriver,_webDriverWait, ServiceCatalogPage.ServiceName, ElementState.VISIBLE);
            IWebElement serviceNameLabel = _webDriver.FindElement(ServiceCatalogPage.ServiceName);

            Assert.AreEqual(p0, serviceNameLabel.Text.ToString());

            Assert.AreEqual("عملية الدفع", _webDriver.FindElement(ServiceCatalogPage.StepOne).Text.ToString());

            Assert.AreEqual("خدمة فورية.", _webDriver.FindElement(ServiceCatalogPage.ProcedureTime).Text.ToString());

            Assert.AreEqual("المواطنين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceFirstCategory).Text.ToString());
            Assert.AreEqual("المقيمين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceSecondCategory).Text.ToString());
            Assert.AreEqual("الزائرين", _webDriver.FindElement(ServiceCatalogPage.TargetAudienceThirdCategory).Text.ToString());

            Assert.AreEqual(2, _webDriver.FindElements(ServiceCatalogPage.EntitesList).Count);
            Assert.AreEqual("بلدية مدينة الشارقة", _webDriver.FindElement(ServiceCatalogPage.secondEntity).Text.ToString());
            Assert.AreEqual("بلدية مدينة خورفكان", _webDriver.FindElement(ServiceCatalogPage.firstEntity).Text.ToString());

            Assert.AreEqual(3, _webDriver.FindElements(ServiceCatalogPage.PaymentMethods).Count);
            Assert.AreEqual("حساب تحصيل", _webDriver.FindElement(ServiceCatalogPage.firstPaymentMethod).Text.ToString());
            Assert.AreEqual("بطاقة ائتمان", _webDriver.FindElement(ServiceCatalogPage.secondPaymentMethod).Text.ToString());
            Assert.AreEqual("تحويل بنكي", _webDriver.FindElement(ServiceCatalogPage.thirdPaymentMethod).Text.ToString());

            Assert.AreEqual(4, _webDriver.FindElements(ServiceCatalogPage.FeesList).Count);

            ReadOnlyCollection<IWebElement> elements = _webDriver.FindElements(ServiceCatalogPage.FeesValues);
            List<string> actualValues = elements.Select(element => element.Text.Trim()).ToList();
            List<string> expectedValues = new List<string> { "2 درهم", "5 درهم", "8 درهم", "12 درهم" };
            Assert.AreEqual(expectedValues.Count, elements.Count);
            CollectionAssert.AreEqual(expectedValues, actualValues);
        }

        [Then(@"Production user click Next for (.*) hour")]
        public void ThenProductionUserclickNextforhour(int args1)
        {
            Thread.Sleep(2000);
            WaitUtils.elementState(_webDriver,_webDriverWait, VehiclesListPage.ParkingDurationNextButton, ElementState.VISIBLE);
            AmountToPay = _webDriver.FindElement(VehiclesListPage.AmountToPay).Text.ToString();
            _webDriver.FindElement(VehiclesListPage.ParkingDurationNextButton).Click();
        }

        [When(@"User Verify the Payment Methods in Payment Page")]
        public void WhenUserVerifyThePaymentMethodsInPaymentPage(Table paymentMethodsTable)
        {
  
                // Wait for the page to load and the relevant elements to be visible
                DriverExtensions.WaitUntilPageLoaded(_webDriver, PageUrls.ProductionPaymentPage);
                Thread.Sleep(999);
                if(  _webDriver.FindElements(PaymentPage.FirstPaymentMethods).Count <=0){
                    _webDriver.Navigate().Refresh();
                    Thread.Sleep(2000);
                 }

                WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.FirstPaymentMethods, ElementState.VISIBLE);
                WaitUtils.elementState(_webDriver,_webDriverWait, PaymentPage.TotalAmountToPay, ElementState.VISIBLE);


                // Assert the total amount to pay
                Assert.AreEqual(AmountToPay, _webDriver.FindElement(PaymentPage.TotalAmountToPay).Text.ToString());

                // Get the list of payment methods on the page
                IList<IWebElement> paymentMethodElements = _webDriver.FindElements(PaymentPage.PaymentMethods);

                 Thread.Sleep(1000);
                // Loop through each row in the Gherkin table
                foreach (var row in paymentMethodsTable.Rows)
                {
                    // Get the expected payment method from the table
                    string expectedPaymentMethod = row["Payment Method"];
                    // Check if the expected payment method is present in the list of elements on the page
                    bool isPaymentMethodPresent = paymentMethodElements.Any(element => element.Text.Trim() == expectedPaymentMethod.Trim());
                    // Assert that the expected payment method is present on the page
                    Assert.IsTrue(isPaymentMethodPresent, $"Expected payment method '{expectedPaymentMethod}' not found on the page.");
                }
        }
    }
}
