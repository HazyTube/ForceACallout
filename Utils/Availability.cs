/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using LSPD_First_Response.Mod.API;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System.Drawing;

namespace ForceACallout.Utils
{
    internal static class Availability
    {
        private static ResRectangle _availableRect;
        private static ResText _availableText;

        internal static void Main()
        {
            var resolutionRatio = UIMenu.GetScreenResolutionMantainRatio();

            //Checks if the option for the on screen text is set to true
            if (Globals.Config.AvailableForCalloutsText)
            {
                if (Globals.Config.PLDCompatibility == true)
                {
                    _availableRect = new ResRectangle(new Point(320, (int)(resolutionRatio.Height - 130)), new Size(200, 50), Color.FromArgb(Globals.Config.RectangleAlpha, Color.Black));
                }
                else
                {
                    _availableRect = new ResRectangle(new Point(320, (int)(resolutionRatio.Height - 67)), new Size(200, 50), Color.FromArgb(Globals.Config.RectangleAlpha, Color.Black));
                }
            }

            if (Globals.Config.PLDCompatibility == true)
            {
                _availableText = new ResText("Available for calls: ",
                    new Point(420, (int)(resolutionRatio.Height - 120)), 0.3f, Color.FromArgb(255, Color.White),
                    RAGENativeUI.Common.EFont.ChaletLondon, ResText.Alignment.Centered);
            }
            else
            {
                _availableText = new ResText("Available for calls: ",
                    new Point(420, (int)(resolutionRatio.Height - 57)), 0.3f, Color.FromArgb(255, Color.White),
                    RAGENativeUI.Common.EFont.ChaletLondon, ResText.Alignment.Centered);
            }


            Game.FrameRender += GameOnFrameRender;
        }

        private static void GameOnFrameRender(object sender, GraphicsEventArgs e)
        {
            //Checks if the option for the on screen text is set to true
            if (Globals.Config.AvailableForCalloutsText) _availableRect.Draw();

            //Checks if the option for the on screen text is set to true
            if (Globals.Config.AvailableForCalloutsText)
            {
                _availableText.Draw();
            }
            //Checks if the player is available for calls or not, if the player is available it draws the on screen text
            if (Functions.IsPlayerAvailableForCalls()) _availableText.Caption = _availableText.Caption = "Available for calls: Yes";
            else _availableText.Caption = _availableText.Caption = "Available for calls: No";
        }
    }
}
