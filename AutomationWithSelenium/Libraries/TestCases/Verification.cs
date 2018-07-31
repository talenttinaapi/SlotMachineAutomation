
using OpenQA.Selenium;
using AutomationWithSelenium.Libraries.Objects;
using System.Collections.Generic;
using System.Linq;

namespace AutomationWithSelenium
{
    public class Verification
    {
 

        /// <summary>
        /// Verify text in element 
        /// </summary>
        /// <param name="pElement">Element Name</param> 
        /// <param name="text">Text to be verified</param>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>

        public static void VerifyElementText(By pElement, string text, ref bool pResult, ref string pMsg)
        {
            IWebElement el = ConstantsLib.Driver.FindElement(pElement);

            if (el != null)
            {
                if (el.Text != text)
                {
                    pResult = false;
                    pMsg += "Text of "+ pElement.ToString()+" doesn't match.\r\n";
                    
                }

            }
            else
            {
                pResult = false;
                pMsg += "No element.\r\n";
            }

        }


        /// <summary>
        /// Verify Win Chart Amounts are updated correctly after betting
        /// </summary>
        /// <param name="game">Reference to game satus</param> 
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyWinChartUpdatedAfterBet(ref Game game, ref bool pResult, ref string pMsg)
        {
            Dictionary<string, WinChartAttributes> init = game.InitPrizeList;
            Dictionary<string, WinChartAttributes> current = game.GetPrizeList();

            foreach (var entry in init)
            {
                if (entry.Value.Payout == current[entry.Key].Payout / game.Bet)
                {
                    pResult = true;
                }
                else
                {
                    pResult = false;
                    pMsg += "Amount is not updated after bet\r\n";
                    break;
                }
            }
        }


        /// <summary>
        /// Verify Combination of Reels in Machine matches Win Chart row
        /// </summary>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyWinningReels(ref bool pResult, ref string pMsg)
        {
            List<string> actualpositions = new List<string>();
            var ReelContainer = ConstantsLib.Driver.FindElements(By.XPath("//div[@id='ReelContainer']//div[@class='reel']"));
            foreach (IWebElement reel in ReelContainer)
            {
                string tmp = reel.GetAttribute("style");
                actualpositions.Add(tmp.Remove(0, 5).Replace("px;", " ").Trim());
            }


            ExpectedPosition exp = new ExpectedPosition("Skin1");
            string[] arr = new string[3];
            List<string> _tmp;

            var res = false;
            foreach (var entry in exp.List)
            {

                arr[0] = entry[1];
                arr[1] = entry[2];
                arr[2] = entry[3];

                _tmp = new List<string>(arr);

                res= _tmp.SequenceEqual(actualpositions);
                
                if(res==true)
                {
                    string id = entry[0];
                    IWebElement row = ConstantsLib.Driver.FindElement(By.Id(id));
                    if(row.GetAttribute("class")== "trPrize won")
                    {
                        pResult = true;
                        break;
                    }
                    else
                    {
                        pResult = false;
                        pMsg += "Actual Result doesn't match Win Chart!";
                        break;
                    }
                }
         
            }

            if(res==false)
            {
                pResult = false;
                pMsg += "Actual Result doesn't exist on Win Chart!";
            }

        }


        /// <summary>
        /// Verify Reel Icons matches style
        /// </summary>
        /// <param name="style">Style of icon</param>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyIcons(string style,ref bool pResult, ref string pMsg)
        {
            pResult = true;
            var ReelContainer = ConstantsLib.Driver.FindElements(By.XPath("//div[@id='ReelContainer']//div[@class='reel']"));
            foreach (IWebElement reel in ReelContainer)
            {

                if (!reel.GetCssValue("background-image").Contains(style))
                {
                    pResult = false;
                    pMsg += "Wrong Reel Icons";
                    break;
                }
            }
        }

        /// <summary>
        /// Verify Backgorund style
        /// </summary>
        /// <param name="id">Style of background</param>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyBackground(string id, ref bool pResult, ref string pMsg)
        {
            var bgd = ConstantsLib.Driver.FindElements(By.XPath("//div[@class='changeable_background' and @data-id='" + id + "']"));
            if (!bgd.ElementAt(0).GetAttribute("style").Contains("display: none;"))
            {
                pResult = true;
            }
            else
            {
                pResult = false;
                pMsg += "Wrong Background";
            }
        }


        /// <summary>
        /// Verify Machine style
        /// </summary>
        /// <param name="id">Style of machine</param>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyMachine(string id, ref bool pResult, ref string pMsg)
        {

            string xpath = "//div[@id='slotsSelectorWrapper' and @class='reelSet1 slotMachine" + id + "']";
            var MachineContainer = ConstantsLib.Driver.FindElements(By.XPath(xpath));

            if(MachineContainer.Count!=0)
            {
                pResult = true;
            }
            else
            {
                pResult = false;
                pMsg += "Wrong Machine";
            }
        }


        /// <summary>
        /// Verify Winning Banner displayed
        /// </summary>
        /// <param name="url">url to banner</param>
        /// <param name="pResult">Flag that indicates result of the test case</param>
        /// <param name="pMsg">Message of failure</param>
        public static void VerifyWinBannerDisplayed(string url, ref bool pResult, ref string pMsg)
        {

            IWebElement outerContainer = ConstantsLib.Driver.FindElement(By.Id("SlotsOuterContainer"));

            if (outerContainer.GetAttribute("class")=="won")
            {
                
                var innerContainer = outerContainer.FindElement(By.Id("SlotsInnerContainer"));
                string tmp = innerContainer.GetCssValue("background-image");
                if (innerContainer.GetCssValue("background-image").Contains(url))
                {
                    pResult = true;
                }

                else
                {
                    pResult = false;
                    pMsg += "Wrong Win Banner";
                }

            }
            else
            {
                pResult = false;
                pMsg += "No Win Banner";
            }
        }


    }

}



