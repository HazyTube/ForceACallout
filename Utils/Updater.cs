/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with the on screen text box

*/

using Rage;
using System;
using System.Net;

namespace ForceACallout.Utils
{
    internal static class Updater
    {
        //creates a new webclient
        private static readonly WebClient wc = new WebClient();

        public static int CheckUpdate()
        {
            string response = null;

            try
            { 
                //Gets the latest version from a text file on github
                Logger.DebugLog("Fetching latest plugin version from GitHub");
                response = wc.DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/HazyTube/ForceACallout/master/LatestVersion")).Result;
            }
            catch (Exception)
            {
                //Logs something if there is an exception
                Game.LogTrivial("Checking version of plugin 'Force A Callout' Failed");
            }

            //If we get a null response, the download failed and return -2 and inform the user that the download failed
            if (string.IsNullOrWhiteSpace(response))
            {
                return -2;
            }

            Globals.Application.LatestVersion = response;

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
    }
}
