using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationWithSelenium.Interfaces
{
    class Layout
    {

        public static By imgLogo = By.XPath("//img[@src='img/logo.png']");


    }

    class BetContainer
    {

        public static By btnSpin = By.Id("spinButton");
        public static By btnBetUp = By.Id("betSpinUp");
        public static By btnBetDown = By.Id("betSpinDown");
        public static By txtCredits = By.Id("credits");
        public static By txtBet = By.Id("bet");
        public static By txtLastWin = By.Id("lastWin");

    }

    class WinChartPanel
    {
        public static By tblWinChart = By.Id("prizes_list_slotMachine1");
        
    }

    class CustomizePanel
    {
        public static By btnChangeBackground = By.ClassName("btnChangeBackground");
        public static By btnChangeIcons = By.ClassName("btnChangeReels");
        public static By btnChangeMachine = By.ClassName("btnChangeMachine");
    }

}
