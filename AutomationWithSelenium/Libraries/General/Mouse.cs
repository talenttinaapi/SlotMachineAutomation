using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Remote;

namespace AutomationWithSelenium
{
    public class S_Mouse
    {
        /// <summary>
        /// Click on a web element and wait for page load
        /// </summary>
        /// <param name="pElement">The Web Element is used</param>
        public static void Click(By pBy)
        {
            IWebElement  pElement = F_General.CaptureInterface(pBy);
            ((IJavaScriptExecutor)ConstantsLib.Driver).ExecuteScript("arguments[0].scrollIntoView(true);" + "window.scrollBy(0,-100);", pElement);
            pElement.Click();
            
        }
      
    }
}
