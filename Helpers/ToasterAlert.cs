

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SD.Helpers;
using SD.Helpers.Enums;

namespace SD.Helpers
{
	internal class ToasterAlert
	{
		protected IWebDriver _driver;
		protected IWait<IWebDriver> _wait;

		public static By ToasterBy => By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']");
		public static By ToasterCloseButtonBy => By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']/button");

		public static By ToasterBody =By.XPath("//div[@role='alert' and @class='Toastify__toast-body']");

		public static By ToasterMessage = By.XPath("//*[text()='تم حفظ البيانات بنجاح']");


	}
}

