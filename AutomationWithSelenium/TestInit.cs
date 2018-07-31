using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Net;
using System.Reflection;

namespace AutomationWithSelenium
{
    [CodedUITest]
    public class TestInit : AssemblyInit
    {
        public bool Result = true;
        public string Msg = "\r\n";

        public enum Drivers
        {
            Chrome,
            Firefox,
        }

        public void StartDriver(Drivers driver)
        {
            var ProjectDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = @"..\..\..\WebDrivers";
            var driverPath = Path.GetFullPath(Path.Combine(ProjectDirectory, relativePath));

            switch (driver)
           {
                case Drivers.Chrome:
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArguments("--disable-impl-side-painting");
                ConstantsLib.Driver = new ChromeDriver(driverPath, options);
                break;

                case Drivers.Firefox:
                FirefoxOptions options2 = new FirefoxOptions();
                options2.AddArgument("--start-maximized");
                options2.AddArguments("--disable-impl-side-painting");
                ConstantsLib.Driver = new FirefoxDriver(driverPath, options2);
                break;

            }
        }


        [TestInitialize]
        public void Initialize()
        {

            StartDriver(Drivers.Firefox);
         
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ConstantsLib.Driver.Quit();
        }
    }
}