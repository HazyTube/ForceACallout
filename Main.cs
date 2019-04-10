﻿/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using Rage;
using LSPD_First_Response.Mod.API;
using ForceACallout.Utils;

namespace ForceACallout
{
    public class Main : Plugin
    {
        //this initializes the plugin
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += DutyStateChange;

            //Simple log that let's the user know the plugin loaded succesfully + the assembly version
            Logger.Log("ForceACallout " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialised.");

            //This sets the currentversion
            Globals.Application.CurrentVersion = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            //This sets the config path to /plugins/lspdfr for the ini file
            Globals.Application.ConfigPath = "Plugins/LSPDFR/";

            Globals.Application.LSPDFRFolder = "lspdfr";

        }

        public void DutyStateChange(bool OnDuty)
        {
            //This only runs if the player is onDuty
            if (OnDuty)
            {
                Game.LogTrivial("--------------------------------------ForceACallout startup log--------------------------------------");
                //Checks for an update
                int versionStatus = Updater.CheckUpdate();
                if (versionStatus == -1)
                {
                    Notifier.StartUpNotificationOutdated();
                    Logger.Log("[WARNING] Plugin is out of date. (Current Version: " + Globals.Application.CurrentVersion + ") - (Latest Version: " + Globals.Application.LatestVersion + ")");
                }
                else if (versionStatus == -2)
                {
                    Logger.Log("[WARNING] There was an issue checking plugin versions, the plugin may be out of date!");
                }
                else if (versionStatus == 1)
                {
                    Logger.Log("[WARNING] Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
                }
                else
                {
                    Notifier.StartUpNotification();
                    Logger.Log("Plugin Version v" + Globals.Application.CurrentVersion + " loaded successfully");
                }

                //Loads the config file (.ini file)
                Config.LoadConfig();

                StartPlugin();
            }
        }

        private static void StartPlugin()
        {
            GameFiber.StartNew(delegate
        {
            Logger.DebugLog("New gamefiber started");
            Core.RunPlugin();
        });
        }

        public override void Finally()
        {
            Logger.Log("ForceACallout has been unloaded");
        }

    }
}
