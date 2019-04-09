/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using System;
using LSPD_First_Response.Mod.API;
using Rage;

namespace ForceACallout.Utils
{
    internal static class Core
    {
        private static EventHandler OnCalloutAccepted;
        private static LHandle _onCalloutAccepted;

        public static void RunPlugin()
        {
            //Loads the on screen text
            Availability.Main();

            //Main loop
            while (true)
            {
                GameFiber.Yield();

                //Checks if the option to automatically change the availability is set to true
                if (Globals.Config.AutoChangeAvailability)
                {
                    //If a callout is being displayed
                    if (Functions.IsCalloutRunning())
                    {
                        //Checks if the player presses Y to accept the callout
                        if (Game.IsKeyDown(Globals.Controls.LSPDFRAcceptCalloutKey))
                        {
                            //Sets the availability of the player to unavailable
                            Functions.SetPlayerAvailableForCalls(false);
                        }
                    }
                    else if (Globals.Config.OnlySetToUnavailable == false)
                    {
                        Functions.SetPlayerAvailableForCalls(true);
                    }
                }

                //Checks if the modifier value is set to none
                if (Globals.Controls.ForceCalloutModifier == System.Windows.Forms.Keys.None)
                {
                    //Checks if the key to force a callout is pressed, then starts a random callout and debug logs something
                    if (Game.IsKeyDownRightNow(Globals.Controls.ForceCalloutKey))
                    {
                        Logger.DebugLog("FoceCalloutKey pressed");
                        if (Functions.IsCalloutRunning() && Globals.Config.StopCurrentCallout == true) { Functions.StopCurrentCallout(); }
                        else { RandomCallouts.StartRandomCallout(); }
                    }
                }
                else
                {
                    //If the modifier value is set to something else it will check if the key and modifier is pressed to force a callout, then starts a random callout and logs something
                    if (Game.IsKeyDownRightNow(Globals.Controls.ForceCalloutKey) && Game.IsKeyDownRightNow(Globals.Controls.ForceCalloutModifier))
                    {
                        Logger.DebugLog("FoceCalloutKey + ModifierKey pressed");
                        if (Functions.IsCalloutRunning() && Globals.Config.StopCurrentCallout == true) { Functions.StopCurrentCallout(); }
                        else { RandomCallouts.StartRandomCallout(); }
                    }
                }

                //Checks if the modifier value is set to none
                if (Globals.Controls.AvailabilityModifier == System.Windows.Forms.Keys.None)
                {
                    //Checks if the key to set the player's availabilityKey is pressed, then logs something
                    if (Game.IsKeyDown(Globals.Controls.AvailabilityKey))
                    {
                        Logger.DebugLog("AvailabilityKey Pressed");

                        Globals.Status.FirstEvent = true;

                        //Checks if the player is available for calls
                        if (Functions.IsPlayerAvailableForCalls())
                        {
                            //If the player is available for calls it sets the availability to false
                            Functions.SetPlayerAvailableForCalls(false);

                            //If the on screen text is set to false it will show a notification instead
                            if (Globals.Config.AvailableForCalloutsText == false)
                            {
                                Game.DisplayNotification("You are now ~r~unavailable~s~ for calls");
                            }

                            //Logs something
                            Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                        }
                        //If the player is not available for calls, it will set the availability to true
                        else
                        {
                            Functions.SetPlayerAvailableForCalls(true);

                            //If the on screen text is set to false it will show a notification instead
                            if (Globals.Config.AvailableForCalloutsText == false)
                            {
                                Game.DisplayNotification("You are now ~g~available~s~ for calls");
                            }

                            //Logs something
                            Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                        }
                    }
                }
                //If the modifier key is set to something other than None it will check if the key and modifier is pressed
                else
                {
                    if (Game.IsKeyDown(Globals.Controls.AvailabilityKey) && Game.IsKeyDownRightNow(Globals.Controls.AvailabilityModifier))
                    {
                        Logger.DebugLog("AvailabilityKey + ModifierKey Pressed");

                        Globals.Status.FirstEvent = true;

                        //If the player is available for calls, it will set the availability to false
                        if (Functions.IsPlayerAvailableForCalls())
                        {
                            Functions.SetPlayerAvailableForCalls(false);

                            //If the on screen text is set to false it will show a notification instead
                            if (Globals.Config.AvailableForCalloutsText == false)
                            {
                                Game.DisplayNotification("You are now ~r~unavailable~s~ for calls");
                            }

                            //Logs something
                            Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                        }
                        //If the player is not available for calls, it will set the availability to true
                        else
                        {
                            Functions.SetPlayerAvailableForCalls(true);

                            //If the on screen text is set to false it will show a notification instead
                            if (Globals.Config.AvailableForCalloutsText == false)
                            {
                                Game.DisplayNotification("You are now ~g~available~s~ for calls");
                            }

                            //Logs something
                            Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                        }
                    }
                }
            }
        }
    }
}
