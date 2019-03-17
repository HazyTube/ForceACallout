/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with the on screen text box

*/

using Rage;

namespace ForceACallout.Utils
{
    internal static class Notifier
    {
        //Only include the the name of the plugin, this is the prefix
        private const string NotificationPrefix = "ForceACallout";

        //Log line
        internal static void Notify(string body)
        {
            string notice = string.Format("~b~[{0}]~s~: {1}", NotificationPrefix, body);
            Game.DisplayNotification(notice);
            Logger.DebugLog("Notification Sent.");
        }
    }
}
