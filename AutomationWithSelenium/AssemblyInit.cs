
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationWithSelenium
{
    [CodedUITest]
    public class AssemblyInit
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [AssemblyInitialize]
        public static void Assembly_Init(TestContext context)
        {
            
        }

        [AssemblyCleanup]
        public static void Assembly_Cleanup()
        {
            Process[] processes = Process.GetProcessesByName("chromedriver");

            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}