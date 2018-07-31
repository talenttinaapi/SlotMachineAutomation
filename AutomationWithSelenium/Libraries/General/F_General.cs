using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AutomationWithSelenium
{
    public class F_General
    {
        /// <summary>
        /// Capture the interface
        /// </summary>
        /// <param name="pBy">Provides a mechanism by which to find elements within a document</param>
        /// <returns>Return a IWebElement</returns>
        public static IWebElement CaptureInterface(By pBy)
        {
            try { return ConstantsLib.Driver.FindElement(pBy); }
            catch { return null; }
        }


        public static void WaitForPageLoaded(TimeSpan timeout)
        {
            var wait = new WebDriverWait(ConstantsLib.Driver, TimeSpan.FromSeconds(10));
            wait.Until((wdriver) => (ConstantsLib.Driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static string GetTextElement(By pElement)
        {
            IWebElement el = ConstantsLib.Driver.FindElement(pElement);

            if (el != null)
            {
                return el.Text;

            }
            else
            {
                return null;
            }
        }

        public static void WaitForElementAttributeChangedToExpectedValue(By pBy, string pAttribute, string pExpectedValue, int pTimeout = 0)
        {
            if (pTimeout == 0)
                pTimeout = ConstantsLib.TimeOut;

            DateTime mFrom = DateTime.Now;
            IWebElement mEle = CaptureInterface(pBy);
            DateTime mTo = DateTime.Now;
            while ((mTo - mFrom).Seconds < pTimeout)
            {
                if (!mEle.GetAttribute(pAttribute).Equals(pExpectedValue))
                    Thread.Sleep(2000);
                else break;
            }
        }


    }


}


