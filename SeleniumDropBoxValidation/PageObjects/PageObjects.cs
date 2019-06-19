using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Threading;
using AutoItX3Lib;
using System.IO;
using System.Configuration;

namespace SeleniumDropBoxValidation.PageObjectsDropBox
{
	class PageObjects
	{
		public static IWebDriver rdriver;

		public PageObjects(IWebDriver ldriver)
		{
			rdriver = ldriver;
		}

        AutoItX3 auto = new AutoItX3();

		public void SignInClick()
		{
			rdriver.FindElement(By.XPath("//button[@id='sign-up-in']")).Click();
		}

		public void Email()
		{
			rdriver.FindElement(By.XPath("//input[@name='login_email']")).Clear();
            //rdriver.FindElement(By.XPath("//input[@name='login_email']")).SendKeys("ashishsharmagimt@gmail.com");
            rdriver.FindElement(By.XPath("//input[@name='login_email']")).SendKeys(ConfigurationManager.AppSettings["Username"]);

        }

        public void Password()
		{
			rdriver.FindElement(By.XPath("//input[@name='login_password']")).Clear();
			rdriver.FindElement(By.XPath("//input[@name='login_password']")).SendKeys(ConfigurationManager.AppSettings["Password"]);
		}

		public void RememberMe()
		{
			rdriver.FindElement(By.XPath("//span[contains(text(),'Remember me')]")).Click();
			//rdriver.FindElement(By.CssSelector("css=span#1")).Click;
		}

		public string LoginButton()
		{
			rdriver.FindElement(By.XPath("//div[@class='signin-text']")).Click();
			rdriver.FindElement(By.XPath("//span[contains(@class,'mc-button mc-button-circular')]//div[contains(@class,'mc-avatar')]")).Click();
            string signOut= rdriver.FindElement(By.XPath("//a[contains(text(),'Sign out')]")).Text;
            return signOut;
        }

        public void GetListMenu()
		{
			IList<IWebElement> primaryListMenu=  rdriver.FindElements(By.XPath("//ul[@class='maestro-nav__products']//li"));
			int length = primaryListMenu.Count;
			for (int i = 0; i <= length; i++)
			{
				string textFile = rdriver.FindElement(By.Id("files")).Text;
				if (textFile == "Files")
				{
					rdriver.FindElement(By.Id("files")).Click();
					break;
				}
			}
		}

		public void SubFolderList()
		{
			IList<IWebElement> fileListMenu = rdriver.FindElements(By.XPath("//ul[@class='mc-tertiary-list secondary-action-menu']//li"));
			int length = fileListMenu.Count;
			for (int i = 0; i <= length; i++)
			{
				string subMenu = rdriver.FindElement(By.XPath("//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-new-folder']")).Text;
				//Console.WriteLine(subMenu);
				if (subMenu == "New folder")
				{
					rdriver.FindElement(By.XPath("//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-new-folder']")).Click();
					break;
				}
			}
		}

		public string CreateShareNewFolder()
		{
			rdriver.FindElement(By.XPath("//input[@id='new_folder_name_input']")).SendKeys(ConfigurationManager.AppSettings["Foldername"]);
            rdriver.FindElement(By.XPath("//input[@id='confidential_option']")).Click();
            rdriver.FindElement(By.XPath("//button[@class='button-primary dbmodal-button']")).Click();
            rdriver.FindElement(By.XPath("//input[@placeholder='Email or name']")).SendKeys(ConfigurationManager.AppSettings["sharedEmailId"]);
            rdriver.FindElement(By.XPath("//span[@class='mc-dropdown-button-content']")).Click();
            IList<IWebElement> menu = rdriver.FindElements(By.XPath("//ul[@class='mc-menu']"));
            int length = menu.Count();
            for (int i = 0; i <= length; i++)
            {
                string dropDownMenu = rdriver.FindElement(By.XPath("//div[contains(text(),'Can view')]")).Text;
                if (dropDownMenu == "Can view")
                {
                    rdriver.FindElement(By.XPath("//div[contains(text(),'Can view')]")).Click();
                    break;
                }
            }
            rdriver.FindElement(By.XPath("//textarea[@placeholder='Add a message (optional)']")).SendKeys("This is a test message");
            rdriver.FindElement(By.XPath("//button[@class='scl-sharing-modal-footer-inband__button mc-button mc-button-primary']//span[@class='mc-button-content'][contains(text(),'Share')]")).Click();
            //rdriver.FindElement(By.XPath("//span[@class='ue-effect-container uee-AppActionsView-PrimaryButtonText']")).Click();
            string foldernameText = rdriver.FindElement(By.XPath("//nav[@id='path-breadcrumbs']//span[contains(text(),'1')]")).Text;
            return foldernameText;
        }

        public string ValidateEmail()
        {
            
            string emailName = rdriver.FindElement(By.XPath("//span[contains(text(),'ashish.bond007@gmail.com')]")).Text;
           // rdriver.FindElement(By.XPath("//button[@class='scl-sharing-modal-header__close mc-button-styleless']")).Click();
            return emailName;
           
        }


        public Boolean UploadFiles()
        {
            
            IList<IWebElement> subList = rdriver.FindElements(By.XPath("//ul[@class='mc-tertiary-list secondary-action-menu']//li"));
            int length = subList.Count;
            for (int i = 0; i <= length; i++)
            {
                string actionMenu = rdriver.FindElement(By.XPath("//div[@id='pagelet-2']//li[1]")).Text;
                if (actionMenu == "Upload files")
                {
                    rdriver.FindElement(By.XPath("//div[@id='pagelet-2']//li[1]")).Click();
                    break;
                }
               
            }

            //Selecting the file from the desktop path
            Thread.Sleep(5000);
            auto.WinActivate("Open");
            auto.ControlFocus("Open", "", "Edit1");
            string path = ConfigurationManager.AppSettings["Path"];
            auto.ControlSetText("Open", "", "Edit1", path);
            auto.ControlClick("Open", "", "Button1");
            Thread.Sleep(2000);
            auto.ControlFocus("Open", "", "DirectUIHWND2");
            SendKeys.SendWait("^(a)");
            Thread.Sleep(2000);
            string[] fileList = Directory.GetFiles(path);
            
            List<string> fileNameList = new List<string>();
            for (int i = 0; i < fileList.Length; i++)
            {
                fileNameList.Add(Path.GetFileName(fileList[i]));
            }

            string[] fileNameArray = fileNameList.ToArray();
            auto.ControlClick("Open", "", "Button1");
            Thread.Sleep(20000);
            IList<IWebElement> totalFiles = rdriver.FindElements(By.XPath("//tbody[@class='mc-table-body mc-table-body-culled']//tr"));
            int filesCount = totalFiles.Count;
           // Console.WriteLine(filesCount);
            for (int j = 1; j <= filesCount; j++)
            {
                
                string fileName = rdriver.FindElement(By.XPath("//tr["+(j)+"]//td[2]")).Text;
                Console.WriteLine(fileName);
                if (!fileNameArray.Contains(fileName))
                {
                    return false;
                }

                
            }
            return true;
        }


        }
    }



