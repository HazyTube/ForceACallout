/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with some stuff

*/

using Rage;

namespace ForceACallout.Utils
{
    internal static class Core
    {
        public static void RunPlugin()
        {
            while (true)
            {
                GameFiber.Yield();

                if (Game.IsKeyDownRightNow(Globals.Controls.ForceCalloutKey))
                {
                    Logger.DebugLog("FoceCalloutKey pressed");
                    RandomCallouts.StartRandomCallout();

                }
            }
        }
    }
}
