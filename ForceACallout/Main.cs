/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using Rage;
using System;
using System.Linq;
using System.Reflection;
using ForceACallout.Utils;
using System.Windows.Forms;
using ForceACallout.API;
using LSPD_First_Response.Mod.API;
using Functions = LSPD_First_Response.Mod.API.Functions;

[assembly: Rage.Attributes.Plugin("ForceACallout", Description = "Start a random callout by pressing a key.", Author = "HazyTube")]
namespace ForceACallout
{
    public class Main : Plugin
    {
        //this initializes the plugin
        public override void Initialize()
        {
            //IMPORTANT! This will set if the plugin is in beta or not, and also sets the beta version
            Globals.Application.IsPluginInBeta = false;
            Globals.Application.CurrentBetaVersion = "-b1";
            //------------------------------------------------------------------------------------------
            
            Globals.Application.PluginName = "ForceACallout";
            
            Functions.OnOnDutyStateChanged += DutyStateChange;
            AppDomain.CurrentDomain.AssemblyResolve += LspdfrResolveEventHandler;
            
            Events.OnCalloutAccepted += EventsOnOnCalloutAccepted;

            Logger.Log(Globals.Application.IsPluginInBeta
                ? $"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version}{Globals.Application.CurrentBetaVersion} has been  initialized."
                : $"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version} has been  initialized.");

            //This sets the current version
            Globals.Application.CurrentVersion =
                $"{Assembly.GetExecutingAssembly().GetName().Version}";

            //This sets the config path to /plugins/lspdfr for the ini file
            Globals.Application.ConfigPath = "Plugins/LSPDFR/";
        }

        private static void EventsOnOnCalloutAccepted(LHandle handle)
        {
            if (!Globals.Config.AutoChangeAvailability) return;
            Functions.SetPlayerAvailableForCalls(false);
            Logger.DebugLog("Player accepted callout and AutoChangeAvailability is set to " +
                            Globals.Config.AutoChangeAvailability + ", setting player to unavailable");
        }

        private static void DutyStateChange(bool OnDuty)
        {
            //This only runs if the player is onDuty
            if (!OnDuty) return;
            
            Game.LogTrivial($"--------------------------------------{Globals.Application.PluginName} startup log--------------------------------------");

            if (!Globals.Application.IsPluginInBeta)
            {
                //Checks for an update
                var versionStatus = Updater.CheckUpdate();

                switch (versionStatus)
                {
                    case -1:
                        Notifier.StartUpNotificationOutdated();
                        Logger.Log($"Plugin is out of date. (Current Version: {Globals.Application.CurrentVersion}) - (Latest Version: {Globals.Application.LatestVersion})");
                        break;
                    case -2:
                        Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                        break;
                    case 1:
                        Logger.Log("Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
                        Notifier.StartUpNotification();
                        break;
                    default:
                        Notifier.StartUpNotification();
                        Logger.Log($"Plugin version v{Globals.Application.CurrentVersion} loaded successfully");
                        break;
                }
            }

            if (Globals.Application.IsPluginInBeta)
            {
                //Checks for an update
                var betaVersionStatus = Updater.CheckBetaUpdate();
                var versionStatus = Updater.CheckUpdate();

                if (betaVersionStatus == -1 || versionStatus == -1)
                {
                    Notifier.StartUpNotificationBetaOutdated();
                    Logger.Log($"Plugin is out of date.");
                    Logger.Log(
                        $"(Current Beta Version: {Globals.Application.CurrentVersion}{Globals.Application.CurrentBetaVersion})");
                    Logger.Log(
                        $"(Latest Beta Version: {Globals.Application.LatestVersion}{Globals.Application.LatestBetaVersion})");
                }
                else if (betaVersionStatus == -2 || betaVersionStatus == 0 || versionStatus == -2)
                {
                    Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                }
                else if (betaVersionStatus == 1 || versionStatus == 1)
                {
                    Notifier.StartUpNotificationBeta();
                    Logger.Log(
                        $"Plugin Version v{Globals.Application.CurrentVersion}{Globals.Application.CurrentBetaVersion} loaded successfully");
                    Logger.Log("Plugin is in beta!");
                }
            }

            //Loads the config file (.ini file)
            Settings.LoadSettings();
                
            GameFiber.StartNew(delegate
            {
                Availability.Main();
                    
                if (Common.IsLspdfrPluginRunning("PoliceSmartRadio"))
                {
                    PoliceSmartRadioFunctions.AddActionToButton(new Action(ChangeAvailability),
                        "ChangeAvailability");
                    PoliceSmartRadioFunctions.AddActionToButton(new Action(ForceCallout), "ForceCallout");
                }

                while (true)
                {
                    GameFiber.Yield();

                    if (Common.IsKeyDown(Globals.Controls.EndCalloutModifier, Globals.Controls.EndCalloutKey) && Functions.IsCalloutRunning())
                    {
                        Functions.StopCurrentCallout();
                    }

                    if (Common.IsKeyDown(Globals.Controls.ForceCalloutModifier, Globals.Controls.ForceCalloutKey))
                    {
                        RandomCallouts.StartRandomCallout();
                    }

                    if (!Common.IsKeyDown(Globals.Controls.AvailabilityModifier, Globals.Controls.AvailabilityKey))
                        continue;
                    Logger.DebugLog("AvailabilityKey Pressed");

                    Globals.Status.FirstEvent = true;

                    if (Functions.IsPlayerAvailableForCalls())
                    {
                        Functions.SetPlayerAvailableForCalls(false);

                        if (!Globals.Config.AvailableForCalloutsText)
                        {
                            Notifier.DisplayNotification("Status", "You are ~r~not available~s~ for calls");
                        }

                        Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                    }
                    else
                    {
                        Functions.SetPlayerAvailableForCalls(true);

                        if (!Globals.Config.AvailableForCalloutsText)
                        {
                            Notifier.DisplayNotification("Status", "You are ~g~available~s~ for calls");
                        }

                        Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                    }
                }
            });
        }

        private static void ChangeAvailability()
        {
            if (Functions.IsPlayerAvailableForCalls())
            {
                Functions.SetPlayerAvailableForCalls(false);

                if (!Globals.Config.AvailableForCalloutsText)
                {
                    Notifier.DisplayNotification("Status", "You are ~r~not available~s~ for calls");
                }
            }
            else
            {
                Functions.SetPlayerAvailableForCalls(true);

                if (!Globals.Config.AvailableForCalloutsText)
                {
                    Notifier.DisplayNotification("Status", "You are ~g~available~s~ for calls");
                }
            }
        }

        private static void ForceCallout()
        {
            RandomCallouts.StartRandomCallout();
        }

        public override void Finally()
        {
            Logger.Log("ForceACallout has been unloaded");
        }

        private static Assembly LspdfrResolveEventHandler(object sender, ResolveEventArgs args)
        {
            return Functions.GetAllUserPlugins().FirstOrDefault(allUserPlugin => args.Name.ToLower().Contains(allUserPlugin.GetName().Name.ToLower()));
        }
    }

    internal static class Common
    {
        internal static bool IsKeyDown(Keys ModifierKey, Keys Key)
        {
            return (Game.IsKeyDownRightNow(ModifierKey) || ModifierKey == Keys.None) && Game.IsKeyDown(Key);
        }
        
        internal static bool IsLspdfrPluginRunning(string Plugin, Version minversion = null)
        {
            return LSPD_First_Response.Mod.API.Functions.GetAllUserPlugins().Select(allUserPlugin => allUserPlugin.GetName()).Any(name => name.Name.ToLower() == Plugin.ToLower() && (minversion == (Version) null || name.Version.CompareTo(minversion) >= 0));
        }
    }
}
