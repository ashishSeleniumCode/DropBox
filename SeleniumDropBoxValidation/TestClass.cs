// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SeleniumDropBoxValidation.BaseClass;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using SeleniumDropBoxValidation.PageObjectsDropBox;
using System.Configuration;
using System.IO;

namespace SeleniumDropBoxValidation
{
	[TestFixture]
	public class TestClass: BaseTest
	{
       
        

        [Test, Order(1)]
		public void LoginValidation()
		{
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            PageObjects drop = new PageObjects(driver);
            drop.SignInClick();
			drop.Email();
			drop.Password();
			drop.RememberMe();
			StringAssert.Contains("Sign out", drop.LoginButton(),"Test case is passed");
        }


        [Test, Order(2)]
        public void NewFolderCreatedValidation()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            PageObjects drop = new PageObjects(driver);
            drop.GetListMenu();
            drop.SubFolderList();
            try
            {
                StringAssert.AreEqualIgnoringCase(ConfigurationManager.AppSettings["Foldername"], drop.CreateShareNewFolder(), "Test case is passed");
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
        }



        [Test, Order(3)]
        public void FolderSharedValidation()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            PageObjects drop = new PageObjects(driver);
            //Console.WriteLine(drop.ValidateEmail());
            StringAssert.Contains(ConfigurationManager.AppSettings["sharedEmailId"], drop.ValidateEmail(), "Test case is passed");
            Assert.IsTrue(drop.UploadFiles(), "Test Case is passed");
        }
    }
}
