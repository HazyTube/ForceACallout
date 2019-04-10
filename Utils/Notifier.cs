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
        private const string NotificationPrefix = "ForceACallout";

        internal static void Notify(string body)
        {
            string notice = string.Format("~p~[{0}]~s~: {1}", NotificationPrefix, body);
            Game.DisplayNotification(notice);
            Logger.DebugLog("Notification Sent.");
        }

        internal static void NotifySubtitle(string body)
        {
            string subtitle = string.Format("~p~[{0}]~s~: {1}", NotificationPrefix, body);
            Game.DisplaySubtitle(subtitle);
            Logger.DebugLog("Subtitle Sent.");
        }

        internal static void StartUpNotification()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazTybe", "~b~Has been loaded.");
            Logger.DebugLog("Startup Notification Sent.");
        }

        internal static void StartUpNotificationOutdated()
        {
            Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", NotificationPrefix, "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~by HazTybe", "~r~Plugin is out of date! Please update the plugin.");
            Logger.DebugLog("Startup Notification (Outdated) Sent.");
        }
    }
}
