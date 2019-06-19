using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumDropBoxValidation.BaseClass
{
	public class BaseTest
	{
		public static IWebDriver driver;
		//int a = 1;
		[OneTimeSetUp]
		public void OpenBrowser()

		{
			driver = new ChromeDriver();
			//driver.Url = "https://www.dropbox.com/";
            driver.Url= ConfigurationManager.AppSettings["URL"];
            driver.Manage().Window.Maximize();
		}

		[OneTimeTearDown]
		public void CloseBrowser()

		{
			driver.Quit();
		}

	}
}
