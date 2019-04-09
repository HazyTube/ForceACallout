/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using System.Windows.Forms;

namespace ForceACallout
{
    public class Globals
    {
        internal static class Controls
        {
            public static Keys ForceCalloutKey { get; set; }
            public static Keys ForceCalloutModifier { get; set; }

            public static Keys AvailabilityKey { get; set; }
            public static Keys AvailabilityModifier { get; set; }

            public static Keys LSPDFRAcceptCalloutKey { get; set; }
        }

        internal static class Config
        {
            //GENERAL
            public static bool DebugLogging { get; set; }
            public static bool AvailableForCalloutsText { get; set; }
            public static int RectangleAlpha { get; set; }
            public static bool PLDCompatibility { get; set; }

            //CALLOUTS
            public static bool CalloutProbability { get; set; }
            public static int CalloutProbabilityModifier { get; set; }
            public static bool StopCurrentCallout { get; set; }
            public static bool AutoChangeAvailability { get; set; }
            public static bool OnlySetToUnavailable { get; set; }
        }

        internal static class Application
        {
            public static string ConfigPath { get; set; }
            public static string CurrentVersion { get; set; }
            public static string LatestVersion { get; set; }
            public static bool SettingsLoaded { get; set; }
            public static string LSPDFRFolder { get; set; }
        }

        internal static class Status
        {
            public static bool FirstEvent { get; set; }
        }
    }
}
