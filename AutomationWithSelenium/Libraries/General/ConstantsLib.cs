using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationWithSelenium
{

    public class ConstantsLib
    {
        public static IWebDriver Driver;
        public static WebDriverWait Wait;
        public static int TimeOut = 30;// In seconds
        public const string SlotMachineURL = "http://slotmachinescript.com/";
    }
}