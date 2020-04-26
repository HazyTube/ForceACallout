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
    internal static class Settings
    {
        private static InitializationFile InitialiseFile(string filepath)
        {
            var ini = new InitializationFile(filepath);
            ini.Create();
            return ini;
        }

        public static void LoadSettings()
        {
            //Reads the ini file
            var settings = InitialiseFile(Globals.Application.ConfigPath + "ForceACallout.ini");

            //Makes a new converter to convert strings to keys
            var kc = new KeysConverter();

            //KEYS
            //Reads the keys and modifiers from the ini file
            var ForceCalloutKey = settings.ReadString("Keybindings", "ForceCalloutKey", "X");
            var ForceCalloutModifier = settings.ReadString("Keybindings", "ForceCalloutModifier", "None");
            var AvailabilityKey = settings.ReadString("Keybindings", "AvailabilityKey", "Z");
            var AvailabilityModifier = settings.ReadString("Keybindings", "AvailabilityModifier", "None");
            var EndCalloutKey = settings.ReadString("Keybindings", "EndCalloutKey", "End");
            var EndCalloutModifier = settings.ReadString("Keybindings", "EndCalloutModifier", "None");

            //KEY CONVERTERS
            //Converts strings to keys
            Globals.Controls.ForceCalloutKey = (Keys)kc.ConvertFromString(ForceCalloutKey);
            Globals.Controls.ForceCalloutModifier = (Keys)kc.ConvertFromString(ForceCalloutModifier);
            Globals.Controls.AvailabilityKey = (Keys)kc.ConvertFromString(AvailabilityKey);
            Globals.Controls.AvailabilityModifier = (Keys)kc.ConvertFromString(AvailabilityModifier);
            Globals.Controls.EndCalloutKey = (Keys)kc.ConvertFromString(EndCalloutKey);
            Globals.Controls.EndCalloutModifier = (Keys)kc.ConvertFromString(EndCalloutModifier);

            //GENERAL
            //Reads the values in the General section from the ini file
            Globals.Config.DebugLogging = settings.ReadBoolean("General", "DebugLogging", false);
            Globals.Config.AvailableForCalloutsText = settings.ReadBoolean("General", "AvailableForCalloutsText", true);
            Globals.Config.RectangleAlpha = settings.ReadInt16("General", "RectangleAlpha", 200);
            Globals.Config.PLDCompatibility = settings.ReadBoolean("General", "PLDCompatibility", false);

            //CALLOUTS
            //Reads the values in the Callouts section from the ini file
            Globals.Config.CalloutProbability = settings.ReadBoolean("Callouts", "CalloutProbability", true);
            Globals.Config.CalloutProbabilityModifier = settings.ReadInt16("Callouts", "CalloutProbabilityModifier", 1);
            Globals.Config.AutoChangeAvailability = settings.ReadBoolean("Callouts", "AutoChangeAvailability", false);

            Globals.Application.SettingsLoaded = true;

            //Logs some things
            Logger.Log("[KEYBINDINGS] ForceCalloutKey is set to " + ForceCalloutKey);
            Logger.Log("[KEYBINDINGS] ForceCalloutModifier is set to " + ForceCalloutModifier);
            Logger.Log("[KEYBINDINGS] AvailabilityKey is set to " + AvailabilityKey);
            Logger.Log("[KEYBINDINGS] AvailabilityModifier is set to " + AvailabilityModifier);
            Logger.Log("[GENERAL] DebugLogging is set to " + Globals.Config.DebugLogging);
            Logger.Log("[GENERAL] AvailableForCalloutsText is set to " + Globals.Config.AvailableForCalloutsText);
            Logger.Log("[GENERAL] RectangleAlpha is set to " + Globals.Config.RectangleAlpha);
            Logger.Log("[GENERAL] CalloutProbability is set to " + Globals.Config.CalloutProbability);
            Logger.Log("[GENERAL] CalloutProbabilityModifier is set to " + Globals.Config.CalloutProbabilityModifier);
            Logger.Log("[GENERAL] PLDCompatibility is set to " + Globals.Config.PLDCompatibility);
            Logger.Log("[GENERAL] AutoChangeAvailability is set to " + Globals.Config.AutoChangeAvailability);
            Game.LogTrivial("-----------------------------------------------------------------------------------------------------");
        }
    }
}