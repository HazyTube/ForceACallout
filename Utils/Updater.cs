/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with some stuff

*/

using Rage;
using System;
using System.Net;

namespace ForceACallout.Utils
{
    internal static class Updater
    {
        private static readonly WebClient wc = new WebClient();

        public static int CheckUpdate()
        {
            string response = null;

            try
            {
                Logger.DebugLog("Fetching latest plugin version from GitHub");
                response = wc.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/HazyTube/ForceACallout/master/LatestVersion")).Result;
            }
            catch (Exception)
            {
                Game.LogTrivial("Checking version of plugin 'Force A Callout' Failed");
            }

            if (string.IsNullOrWhiteSpace(response))
            {
                return -2;
            }

            Globals.Application.LatestVersion = response;

            Version current = new Version(Globals.Application.CurrentVersion);
            Version latest = new Version(Globals.Application.LatestVersion);

            if (current.CompareTo(latest) > 0)
            {
                return 1;
            }
            else if (current.CompareTo(latest) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
