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
                Logger.Log("Fetching latest plugin version from GitHub");
                if (Globals.Application.IsPluginInBeta == false)
                {
                    response = wc.DownloadStringTaskAsync(new Uri("https://github.com/HazyTube/ForceACallout/blob/master/PluginVersionInfo/LatestVersion")).Result;
                }
                else if (Globals.Application.IsPluginInBeta == true)
                {
                    response = wc.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/HazyTube/ForceACallout/master/PluginVersionInfo/LatestBetaVersion")).Result;
                }
            }
            catch (Exception)
            {
                Logger.Log($"Checking version of plugin {Globals.Application.PluginName} Failed");
            }

            //If we get a null respone then the download failed and we just return -2 and inform user of failing the download
            if (string.IsNullOrWhiteSpace(response))
            {
                return -2;
            }

            Globals.Application.LatestVersion = response.Trim();

            Version current = new Version(Globals.Application.CurrentVersion);
            Version latest = new Version(Globals.Application.LatestVersion);

            //This is where we're checking the results
            //If the plugin is newer than what's being reported then we'll return 1 (This will just log the issue, no notification)
            //If the plugin is older than what's being reported then we'll return -1(This Logs aswell as displays a notification)
            //If the plugin is the same version as what's being reported than we'll return 0 (This logs & displays notification that it loaded successfully)
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

        public static int CheckBetaUpdate()
        {
            string LatestBetaResponse = null;

            //This gets the latest beta prefix
            try
            {
                Logger.Log("Fetching latest beta version from GitHub");
                LatestBetaResponse = wc.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/HazyTube/ForceACallout/master/PluginVersionInfo/LatestBetaVersionPrefix")).Result;
            }
            catch (Exception)
            {
                Logger.Log($"Getting latest beta version of plugin {Globals.Application.PluginName} Failed");
            }

            //If we get a null respone then the download failed and we just return -2 and inform user of failing the download
            if (string.IsNullOrWhiteSpace(LatestBetaResponse))
            {
                return -2;
            }

            Globals.Application.LatestBetaVersion = LatestBetaResponse.Trim();

            string current = Globals.Application.CurrentBetaVersion;
            string latest = Globals.Application.LatestBetaVersion;

            if (current == latest)
            {
                return 1;
            }
            else if (current != latest)
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
