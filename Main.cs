/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

/*
CHANGELOG NOTEPAD (This is just a reminder for me so I don't forget what I changed with an update)

- 

*/

using Rage;
using System;
using System.Reflection;
using ForceACallout.Utils;
using System.Windows.Forms;
using ForceACallout.API;
using LSPD_First_Response;
using LSPD_First_Response.Mod.API;

[assembly: Rage.Attributes.Plugin("ForceACallout", Description = "Lets players start a random callout by pressing a key.", Author = "HazyTube")]
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
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LSPDFRResolveEventHandler);
            
            Events.OnCalloutAccepted += EventsOnOnCalloutAccepted;

            if (Globals.Application.IsPluginInBeta)
            {
                Logger.Log($"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version.ToString()}{Globals.Application.CurrentBetaVersion} has been  initialized.");
            }
            else
            {
                Logger.Log($"{Globals.Application.PluginName} {Assembly.GetExecutingAssembly().GetName().Version.ToString()} has been  initialized.");
            }

            //This sets the currentversion
            Globals.Application.CurrentVersion =
                $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            //This sets the config path to /plugins/lspdfr for the ini file
            Globals.Application.ConfigPath = "Plugins/LSPDFR/";
        }

        private void EventsOnOnCalloutAccepted(LHandle handle)
        {
            if (Globals.Config.AutoChangeAvailability)
            {
                Functions.SetPlayerAvailableForCalls(false);
                Logger.DebugLog("Player accepted callout and AutoChangeAvailability is set to " +
                                Globals.Config.AutoChangeAvailability + ", setting player to unavailable");
                
                if (!Globals.Config.AvailableForCalloutsText)
                {
                    Notifier.DisplayNotification("Status", "You are now ~r~not available~s~ for calls");
                }
            }
        }

        public void DutyStateChange(bool OnDuty)
        {
            //This only runs if the player is onDuty
            if (OnDuty)
            {
                Game.LogTrivial($"--------------------------------------{Globals.Application.PluginName} startup log--------------------------------------");

                if (!Globals.Application.IsPluginInBeta)
                {
                    //Checks for an update
                    int versionStatus = Updater.CheckUpdate();

                    if (versionStatus == -1)
                    {
                        Notifier.StartUpNotificationOutdated();
                        Logger.Log($"Plugin is out of date. (Current Version: {Globals.Application.CurrentVersion}) - (Latest Version: {Globals.Application.LatestVersion})");
                    }
                    else if (versionStatus == -2)
                    {
                        Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                    }
                    else if (versionStatus == 1)
                    {
                        Logger.Log("Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
                        Notifier.StartUpNotification();
                    }
                    else
                    {
                        Notifier.StartUpNotification();
                        Logger.Log($"Plugin version v{Globals.Application.CurrentVersion} loaded succesfully");
                    }
                }

                if (Globals.Application.IsPluginInBeta)
                {
                    //Checks for an update
                    int betaVersionStatus = Updater.CheckBetaUpdate();
                    int versionStatus = Updater.CheckUpdate();

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
                    
                    if (Common.IsLSPDFRPluginRunning("PoliceSmartRadio"))
                    {
                        PoliceSmartRadio.API.Functions.AddActionToButton(new Action(ChangeAvailability),
                            "ChangeAvailability");
                        PoliceSmartRadio.API.Functions.AddActionToButton(new Action(ForceCallout), "ForceCallout");
                    }

                    while (true)
                    {
                        GameFiber.Yield();

                        if (Common.IsKeyDown(Globals.Controls.ForceCalloutModifier, Globals.Controls.ForceCalloutKey))
                        {
                            RandomCallouts.StartRandomCallout();
                        }

                        if (Common.IsKeyDown(Globals.Controls.AvailabilityModifier, Globals.Controls.AvailabilityKey))
                        {
                            Logger.DebugLog("AvailabilityKey Pressed");

                            Globals.Status.FirstEvent = true;

                            if (Functions.IsPlayerAvailableForCalls())
                            {
                                Functions.SetPlayerAvailableForCalls(false);

                                if (!Globals.Config.AvailableForCalloutsText)
                                {
                                    Notifier.DisplayNotification("Status", "You are now ~r~not available~s~ for calls");
                                }

                                Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                            }
                            else
                            {
                                Functions.SetPlayerAvailableForCalls(true);

                                if (!Globals.Config.AvailableForCalloutsText)
                                {
                                    Notifier.DisplayNotification("Status", "You are now ~g~available~s~ for calls");
                                }

                                Logger.DebugLog("CallAvailability is set to " + Functions.IsPlayerAvailableForCalls());
                            }
                        }
                    }
                });
            }
        }

        internal static void ChangeAvailability()
        {
            if (Functions.IsPlayerAvailableForCalls())
            {
                Functions.SetPlayerAvailableForCalls(false);

                if (!Globals.Config.AvailableForCalloutsText)
                {
                    Notifier.DisplayNotification("Status", "You are now ~r~not available~s~ for calls");
                }
            }
            else
            {
                Functions.SetPlayerAvailableForCalls(true);

                if (!Globals.Config.AvailableForCalloutsText)
                {
                    Notifier.DisplayNotification("Status", "You are now ~g~available~s~ for calls");
                }
            }
        }

        internal static void ForceCallout()
        {
            RandomCallouts.StartRandomCallout();
        }

        public override void Finally()
        {
            Logger.Log("ForceACallout has been unloaded");
        }
        
        public static Assembly LSPDFRResolveEventHandler(object sender, ResolveEventArgs args)
        {
            foreach (Assembly allUserPlugin in Functions.GetAllUserPlugins())
            {
                if (args.Name.ToLower().Contains(allUserPlugin.GetName().Name.ToLower()))
                    return allUserPlugin;
            }
            return (Assembly) null;
        }
    }

    internal class Common
    {
        internal static bool IsKeyDown(Keys ModifierKey, Keys Key)
        {
            return (Game.IsKeyDownRightNow(ModifierKey) || ModifierKey == Keys.None) && Game.IsKeyDown(Key);
        }
        
        internal static bool IsLSPDFRPluginRunning(string Plugin, Version minversion = null)
        {
            foreach (Assembly allUserPlugin in LSPD_First_Response.Mod.API.Functions.GetAllUserPlugins())
            {
                AssemblyName name = allUserPlugin.GetName();
                if (name.Name.ToLower() == Plugin.ToLower() && (minversion == (Version) null || name.Version.CompareTo(minversion) >= 0))
                    return true;
            }
            return false;
        }
    }
}
