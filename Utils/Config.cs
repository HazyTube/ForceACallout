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
using System.Windows.Forms;

namespace ForceACallout.Utils
{
    class Config
    {
        private static InitializationFile initialiseFile(string filepath)
        {
            InitializationFile ini = new InitializationFile(filepath);
            ini.Create();
            return ini;
        }

        public static string GetConfigFile(int count)
        {
            InitializationFile settings = initialiseFile(Globals.Application.ConfigPath + "ForceACallout.ini");

            var tmp = "";

            //We're just checking if the string is set to nothing because this is an indicator that it wasn't set in the config file thus something is wrong.
            if (tmp == "")
            {
                Game.LogTrivial(Globals.Application.ConfigPath);
            }

            return tmp;
        }

        public static void LoadConfig()
        {
            //Reads the ini file
            InitializationFile settings = initialiseFile(Globals.Application.ConfigPath + "ForceACallout.ini");

            //Makes a new converter to convert strings to keys
            KeysConverter kc = new KeysConverter();

            string FoceCalloutKey;
            string ForceCalloutModifier;
            string AvailabilityKey;
            string AvailabilityModifier;

            //KEYS
            //Reads the keys and modifiers from the ini file
            FoceCalloutKey = settings.ReadString("Keybindings", "ForceCalloutKey", "X");
            ForceCalloutModifier = settings.ReadString("Keybindings", "ForceCalloutModifier", "None");
            AvailabilityKey = settings.ReadString("Keybindings", "AvailabilityKey", "Z");
            AvailabilityModifier = settings.ReadString("Keybindings", "AvailabilityModifier", "None");

            //KEY CONVERTERS
            //Converts strings to keys
            Globals.Controls.ForceCalloutKey = (Keys)kc.ConvertFromString(FoceCalloutKey);
            Globals.Controls.ForceCalloutModifier = (Keys)kc.ConvertFromString(ForceCalloutModifier);
            Globals.Controls.AvailabilityKey = (Keys)kc.ConvertFromString(AvailabilityKey);
            Globals.Controls.AvailabilityModifier = (Keys)kc.ConvertFromString(AvailabilityModifier);

            //GENERAL
            //Reads the values in the General section from the ini file
            Globals.Application.DebugLogging = settings.ReadBoolean("General", "DebugLogging", false);
            Globals.Application.AvailableForCalloutsText = settings.ReadBoolean("General", "AvailableForCalloutsText", true);
            Globals.Application.RectangleAlpha = settings.ReadInt16("General", "RectangleAlpha", 200);
            Globals.Application.CalloutProbability = settings.ReadBoolean("General", "CalloutProbability", true);
            Globals.Application.CalloutProbabilityModifier = settings.ReadInt16("General", "CalloutProbabilityModifier", 1);
            Globals.Application.StopCurrentCallout = settings.ReadBoolean("General", "StopCurrentCallout", true);
            Globals.Application.PLDCompatibility = settings.ReadBoolean("General", "PLDCompatibility", false);

            //Value setter
            Globals.Application.SettingsLoaded = true;

            //Logs some things
            Logger.Log("[KEYBINDINGS] ForceCalloutKey is set to " + FoceCalloutKey);
            Logger.Log("[KEYBINDINGS] ForceCalloutModifier is set to " + ForceCalloutModifier);
            Logger.Log("[KEYBINDINGS] AvailabilityKey is set to " + AvailabilityKey);
            Logger.Log("[KEYBINDINGS] AvailabilityModifier is set to " + AvailabilityModifier);
            Logger.Log("[GENERAL] DebugLogging is set to " + Globals.Application.DebugLogging);
            Logger.Log("[GENERAL] AvailableForCalloutsText is set to " + Globals.Application.AvailableForCalloutsText);
            Logger.Log("[GENERAL] RectangleAlpha is set to " + Globals.Application.RectangleAlpha);
            Logger.Log("[GENERAL] CalloutProbability is set to " + Globals.Application.CalloutProbability);
            Logger.Log("[GENERAL] CalloutProbabilityModifier is set to " + Globals.Application.CalloutProbabilityModifier);
            Logger.Log("[GENERAL] StopCurrentCallout is set to " + Globals.Application.StopCurrentCallout);
            Logger.Log("[GENERAL] PLDCompatibility is set to " + Globals.Application.PLDCompatibility);
        }
    }
}
