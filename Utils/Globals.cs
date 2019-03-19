/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
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
        }

        internal static class Application
        {
            public static string ConfigPath { get; set; }
            public static string CurrentVersion { get; set; }
            public static string LatestVersion { get; set; }

            public static bool DebugLogging { get; set; }
            public static bool AvailableForCalloutsText { get; set; }
            public static int RectangleAlpha { get; set; }
        }
    }
}
