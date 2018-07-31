using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomationWithSelenium.Libraries.Objects;
using AutomationWithSelenium.Interfaces;
using System;
using System.Threading;
using System.Collections.Generic;


namespace AutomationWithSelenium.Tests
{
    [CodedUITest]
    public class TestCase : TestInit
    {

        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyHomePageOpen()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            Game game = new Game();
            Verification.VerifyElementText(BetContainer.txtBet, "1", ref Result, ref Msg);
            Verification.VerifyElementText(BetContainer.txtCredits, game.Credits.ToString(), ref Result, ref Msg);
            Verification.VerifyElementText(BetContainer.txtLastWin, "", ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }

        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyBetUp()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            Dictionary<String, WinChartAttributes> InitPrizeList = game.InitPrizeList;

            for (int i=1;i<10;i++)
            {
                game.BetNumber(i + 1);
                Verification.VerifyElementText(BetContainer.txtBet, Convert.ToString(i+1), ref Result, ref Msg);
                Verification.VerifyWinChartUpdatedAfterBet(ref game, ref Result, ref Msg);
                Thread.Sleep(1000);
            }

            Assert.IsTrue(Result, Msg);

        }

        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyBetDown()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.BetNumber(10);

            for (int i = 10; i == 2; i--)
            {
                game.BetNumber(i-1);
                Verification.VerifyElementText(BetContainer.txtBet, Convert.ToString(i-1), ref Result, ref Msg);
                Verification.VerifyWinChartUpdatedAfterBet(ref game, ref Result, ref Msg);
                Thread.Sleep(1000);
            }

            Assert.IsTrue(Result, Msg);
        }


        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifySpinWithWin()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.BetNumber(1);
            game.SpinWithWin();
            Verification.VerifyElementText(BetContainer.txtCredits, game.Credits.ToString(), ref Result, ref Msg);
            Verification.VerifyElementText(BetContainer.txtLastWin, game.LastWin.ToString(), ref Result, ref Msg);
            Verification.VerifyWinningReels(ref Result, ref Msg);
            Verification.VerifyWinBannerDisplayed(Images.WinBanner,ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }

        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifySpinWithoutWin()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.BetNumber(3);
            game.SpinWithoutWin();
            Verification.VerifyWinBannerDisplayed(Images.WinBanner, ref Result, ref Msg);
            Assert.IsFalse(Result, Msg);
        }


        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyCreditsReducedAfterSpin()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.BetNumber(1);
            S_Mouse.Click(BetContainer.btnSpin);
            Verification.VerifyElementText(BetContainer.txtCredits, (game.Credits - game.Bet).ToString(), ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }


        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyChangingIcons()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.ChangeReelIcons(Images.ReelStyle3);
            Verification.VerifyIcons(Images.ReelStyle3, ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }


        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyChangingBackground()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.ChangeBackground("3");
            Verification.VerifyBackground("3", ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }


        [TestMethod, TestCategory("General"), Priority(1)]
        public void VerifyChangingMachine()
        {
            ConstantsLib.Driver.Navigate().GoToUrl(ConstantsLib.SlotMachineURL);
            F_General.WaitForPageLoaded(TimeSpan.FromSeconds(20));
            Game game = new Game();
            game.ChangeMachine("5");
            Verification.VerifyMachine("5", ref Result, ref Msg);
            Assert.IsTrue(Result, Msg);
        }

    }
}
