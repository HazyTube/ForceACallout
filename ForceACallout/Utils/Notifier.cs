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
using System.Reflection;

namespace ForceACallout.Utils
{
    internal static class Notifier
    {
        private const string NotificationPrefix = "Force A Callout";

        internal static void DisplayNotification(string Subtitle, string Body)
        {
            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", NotificationPrefix, $"~b~{Subtitle}~s~",
                Body);
            Logger.DebugLog("Notification Displayed");
        }

        internal static void StartUpNotification()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazyTube", "~b~Has been loaded.");
            Logger.DebugLog("Startup Notification Sent.");
        }

        internal static void StartUpNotificationOutdated()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazyTube", "~r~Plugin is out of date! Please update the plugin.");
            Logger.DebugLog("Startup Notification (Outdated) Sent.");
        }
        
        internal static void StartUpNotificationBeta()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + Globals.Application.CurrentBetaVersion + "~s~ ~o~by HazyTube", "~b~Has been loaded.~s~ \nPlugin is in beta!");
            Logger.DebugLog("Startup Notification (Beta) Sent.");
        }

        internal static void StartUpNotificationBetaOutdated()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + Globals.Application.CurrentBetaVersion + "~s~ ~o~by HazyTube", $"~r~Plugin is out of date!~s~ \nPlease download the latest beta version from GitHub. \nLatest Version: {Globals.Application.LatestVersion}{Globals.Application.LatestBetaVersion}");
            Logger.DebugLog("Startup Notification (Beta Outdated) Sent.");
        }
    }
}
