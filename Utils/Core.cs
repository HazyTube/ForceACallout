/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with some stuff

*/

using LSPD_First_Response.Mod.API;
using Rage;
using Rage.Native;

namespace ForceACallout.Utils
{
    internal static class Core
    {
        public static void RunPlugin()
        {
            while (true)
            {
                GameFiber.Yield();

                //Checks if the key from the ini file is pressed
                if (Game.IsKeyDownRightNow(Globals.Controls.ForceCalloutKey))
                {
                    //Starts a random callout
                    Logger.DebugLog("FoceCalloutKey pressed");
                    RandomCallouts.StartRandomCallout();

                }

                //Checks if the AvailabilityKey is pressed
                if (Game.IsKeyDown(Globals.Controls.AvailabilityKey))
                {
                    Logger.DebugLog("AvailabilityKey Pressed");

                    //If the player is available for calls (true) we set the availability to unavailable (false)
                    if (Functions.IsPlayerAvailableForCalls())
                    {
                        Functions.SetPlayerAvailableForCalls(false);
                        Game.DisplayNotification("You are now ~r~unavailable~s~ for calls");
                        Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                    }
                    else
                    {
                        Functions.SetPlayerAvailableForCalls(true);
                        Game.DisplayNotification("You are now ~g~available~s~ for calls");
                        Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                    }
                }
            }
        }
    }
}
