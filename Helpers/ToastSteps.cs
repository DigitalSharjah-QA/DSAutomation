using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SD.Helpers;

namespace SD.Helpers
{
	[Binding]
	public class ToastSteps 
	{


		public static void AssertAToasterIsDisplayedWithMessage(WebDriverWait _webDriverWait,string message)
		{
			_webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ToasterAlert.ToasterBody));
		}







	}
}