using System;
using System.Collections.Generic;
using System.Linq;
using AutomationWithSelenium.Interfaces;
using OpenQA.Selenium;
using System.Threading;
 using SeleniumExtras.WaitHelpers;



namespace AutomationWithSelenium.Libraries.Objects
{
    public class Game
    {
        public int Bet;
        public int Credits;
        public int Lastwin;
        public Dictionary<string, WinChartAttributes> InitPrizeList = new Dictionary<string, WinChartAttributes>();

        public Game(int betNumber)
        {
            Credits = Convert.ToInt32(F_General.CaptureInterface(BetContainer.txtCredits).Text);
            Bet = betNumber;
            InitPrizeList = GetPrizeList();
        }

        public Game()
        {
            Bet = 1;
            Credits = Convert.ToInt32(F_General.CaptureInterface(BetContainer.txtCredits).Text);
            InitPrizeList = GetPrizeList();
        }

        public void BetNumber(int number)
        {
            Bet = number;
            int startNumber = Convert.ToInt32(F_General.GetTextElement(BetContainer.txtBet));
            if (startNumber < Bet)
            {
                while (Convert.ToInt32(F_General.GetTextElement(BetContainer.txtBet)) < Bet)
                {
                    S_Mouse.Click(BetContainer.btnBetUp);
                }
            }
            if (startNumber > Bet)
            {
                while (Convert.ToInt32(F_General.GetTextElement(BetContainer.txtBet)) > Bet)
                {
                    S_Mouse.Click(BetContainer.btnBetDown);
                }
            }

        }


        public void ChangeReelIcons(string style)
        {

            var ReelContainer = ConstantsLib.Driver.FindElements(By.XPath("//div[@id='ReelContainer']//div[@class='reel']"));
            foreach (IWebElement reel in ReelContainer)
            {
               
                while (!reel.GetCssValue("background-image").Contains(style))
                {
                    S_Mouse.Click(CustomizePanel.btnChangeIcons);
                    Thread.Sleep(1000);
                }
            }
        }

        public void ChangeBackground(string id)
        {

            var bgd = ConstantsLib.Driver.FindElements(By.XPath("//div[@class='changeable_background' and @data-id='"+id+"']"));
            while (bgd.ElementAt(0).GetAttribute("style").Contains("display: none;"))
            {
                 S_Mouse.Click(CustomizePanel.btnChangeBackground);
                 Thread.Sleep(1000);
       
            }
        }


        public void ChangeMachine(string id)
        {
            string xpath = "//div[@id='slotsSelectorWrapper' and @class='reelSet1 slotMachine" + id + "']";
            var MachineContainer = ConstantsLib.Driver.FindElements(By.XPath(xpath));
            while (MachineContainer.Count==0)
            {
                    S_Mouse.Click(CustomizePanel.btnChangeMachine);
                    Thread.Sleep(1000);
                    MachineContainer = ConstantsLib.Driver.FindElements(By.XPath(xpath));
            }

        }


        public void SpinWithRounds(int rounds)
        {
            Thread.Sleep(1000);
            for (int i = 0; i < rounds; i++)
            {

                S_Mouse.Click(BetContainer.btnSpin);
                Thread.Sleep(1000);
                F_General.WaitForElementAttributeChangedToExpectedValue(BetContainer.btnSpin, "class", "", 20000);
            }
        }

        public void SpinWithWin()
        {

            IWebElement el = null;
            while (el == null)
            {
                Thread.Sleep(1000);
                S_Mouse.Click(BetContainer.btnSpin);
                Credits = Credits - Bet;
                Thread.Sleep(1000);
                F_General.WaitForElementAttributeChangedToExpectedValue(BetContainer.btnSpin, "class", "", 20000);
                try
                {
                    el = ConstantsLib.Driver.FindElement(By.XPath("//div[@class='trPrize won']"));
                }
                catch (NoSuchElementException) { }
            }

            LastWin = Convert.ToInt32(F_General.CaptureInterface(BetContainer.txtLastWin).Text);
        }

        public void SpinWithoutWin()
        {
            IWebElement el = null;
            while (el == null)
            {
                Thread.Sleep(1000);
                S_Mouse.Click(BetContainer.btnSpin);
                Thread.Sleep(1000);
                F_General.WaitForElementAttributeChangedToExpectedValue(BetContainer.btnSpin, "class", "", 20000);
                try
                {
                    el = ConstantsLib.Driver.FindElement(By.XPath("//div[@class='trPrize won']"));
                    el = null;
                }
                catch (NoSuchElementException) { break; }
            }
        }


        public Dictionary<String, WinChartAttributes> GetPrizeList()
        {
            Dictionary<String, WinChartAttributes> list = new Dictionary<String, WinChartAttributes>();
            var Container = ConstantsLib.Driver.FindElements(By.XPath("//div[@id='prizes_list_slotMachine1']/div"));
            foreach (IWebElement row in Container)
            {
                var Amount = Convert.ToDouble(row.FindElement(By.ClassName("tdPayout")).Text);
                list.Add(row.GetAttribute("id"), new WinChartAttributes { Class= row.GetAttribute("class"), Payout=Amount } );
            }

            return list;
        }


        public  int LastWin
        {
            get
            {
                return Lastwin;
            }
            set
            {
                Lastwin = value;
                Credits = Credits + Lastwin;
            }
        }

    }

    public class WinChartAttributes
    {
        public string Class { get; set; }
        public double Payout{ get; set; }

    }

    public static class Icon
    {

        public static string BigWin;
        public static string BAR;
        public static string Seven;
        public static string Cherry;
        public static string Watermelon;
        public static string Banana;
        public static string BLANK;
    }

    public static class Images
    {

        public static string ReelStyle1= "/img/slotmachine1/reel_strip.png";
        public static string ReelStyle2= "/img/slotmachine2/reel_strip.png";
        public static string ReelStyle3= "/img/slotmachine4/reel_strip.png";
        public static string WinBanner= "/img/slotmachine1/won_bg.png";

    }

    public class ExpectedPosition
    {

        public List<List<string>> List = new List<List<string>>();
 
        
        public ExpectedPosition(string skin)
        {
   
           switch(skin)
            {
                case "Skin1":
                Icon.BigWin = "-1234";
                Icon.BAR = "-994";
                Icon.Cherry = "-1114";
                Icon.Banana = "-634";
                Icon.Watermelon = "-874";
                Icon.Seven = "-754";
                break;
            }


            List.Add(new List<string> { "trPrize_32", Icon.BigWin, Icon.BigWin, Icon.BigWin });       //200
            List.Add(new List<string> { "trPrize_33", Icon.BAR, Icon.BAR, Icon.BAR });          //50
            List.Add(new List<string> { "trPrize_37", Icon.Seven, Icon.Seven, Icon.Seven });          //20
            List.Add(new List<string> { "trPrize_40", Icon.Watermelon, Icon.Cherry, Icon.BAR });        //16
            List.Add(new List<string> { "trPrize_40", Icon.Watermelon, Icon.Seven, Icon.BigWin });
            List.Add(new List<string> { "trPrize_40", Icon.Watermelon, Icon.Seven, Icon.BAR });
            List.Add(new List<string> { "trPrize_40", Icon.Watermelon, Icon.Cherry, Icon.BigWin });
            List.Add(new List<string> { "trPrize_40", Icon.Banana, Icon.Cherry, Icon.BAR });
            List.Add(new List<string> { "trPrize_40", Icon.Banana, Icon.Seven, Icon.BigWin });
            List.Add(new List<string> { "trPrize_40", Icon.Banana, Icon.Seven, Icon.BAR });
            List.Add(new List<string> { "trPrize_40", Icon.Banana, Icon.Cherry, Icon.BigWin });
            List.Add(new List<string> { "trPrize_34", Icon.Cherry, Icon.Cherry, Icon.Cherry });      //15
            List.Add(new List<string> { "trPrize_38", Icon.Banana, Icon.Banana, Icon.Banana });         //14
            List.Add(new List<string> { "trPrize_35", Icon.Watermelon, Icon.Watermelon, Icon.Watermelon });         //12
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Banana, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Banana, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Banana, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Cherry, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Cherry, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Watermelon, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Watermelon, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Cherry, Icon.Watermelon, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Banana, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Banana, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Banana, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Cherry, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Cherry, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Watermelon, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Watermelon, Icon.Watermelon, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Banana, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Banana, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Cherry, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Cherry, Icon.Watermelon });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Watermelon, Icon.Banana });         //7
            List.Add(new List<string> { "trPrize_41", Icon.Banana, Icon.Watermelon, Icon.Cherry });         //7
            List.Add(new List<string> { "trPrize_45", Icon.BLANK, Icon.BLANK, Icon.BLANK });         //4

        }





    }

           


}
