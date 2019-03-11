﻿/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with some stuff

*/

using Rage;
using LSPD_First_Response.Mod.API;
using ForceACallout.Utils;

namespace ForceACallout
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += DutyStateChange;
            Logger.Log("ForceAcAllout " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialised.");
            Globals.Application.CurrentVersion = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            Globals.Application.ConfigPath = "Plugins/LSPDFR/";

        }

        public void DutyStateChange(bool OnDuty)
        {
            if (OnDuty)
            {
                int versionStatus = Updater.CheckUpdate();
                if (versionStatus == -1)
                {
                    Notifier.Notify("Plugin is out of date! (Current Version: ~r~" + Globals.Application.CurrentVersion + " ~s~) - (Latest Version: ~g~" + Globals.Application.LatestVersion + "~s~) Please update the plugin!");
                    Logger.Log("Plugin is out of date. (Current Version: " + Globals.Application.CurrentVersion + ") - (Latest Version: " + Globals.Application.LatestVersion + ")");
                }
                else if (versionStatus == -2)
                {
                    Logger.Log("There was an issue checking plugin versions, the plugin may be out of date!");
                }
                else if (versionStatus == 1)
                {
                    Logger.Log("Current version of plugin is higher than the version reported on the official GitHub, this could be an error that you may want to report!");
                }
                else
                {
                    Notifier.Notify("Plugin loaded ~g~successfully~s~!");
                    Logger.Log("Plugin Version v" + Globals.Application.CurrentVersion + " loaded successfully");
                }

                Config.LoadConfig();

                StartPlugin();
            }
        }

        private static void StartPlugin()
        {
            GameFiber.StartNew(delegate
        {
            Logger.DebugLog("New gamefiber started");
            Core.RunPlugin(); });
        }

        public override void Finally()
        {
            Logger.Log("ForceACallout has been unloaded");
        }

    }
}