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

namespace ForceACallout.Utils
{
internal static class Logger
    {
        //Only include the plugin name.
        private const string LogPrefix = "ForceACallout";

        //Log line
        internal static void Log(string LogLine)
        {
            string log = string.Format("[{0}]: {1}", LogPrefix, LogLine);

            Game.LogTrivial(log);
        }

        //Log line that will only run if the setting for debug logging is enabled
        internal static void DebugLog(string LogLine)
        {
            if (Globals.Application.DebugLogging == true)
            {
                string log = string.Format("[{0}][DEBUG]: {1}", LogPrefix, LogLine);

                Game.LogTrivial(log);
            }
        }
    }
}
