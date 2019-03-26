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

            //Reads the value for debug logging from the ini file
            Globals.Application.DebugLogging = settings.ReadBoolean("General", "DebugLogging", false);

            //Reads the value to enable or disable the on screen text
            Globals.Application.AvailableForCalloutsText = settings.ReadBoolean("General", "AvailableForCalloutsText", true);

            //Reads the int for the rectangle alpha
            Globals.Application.RectangleAlpha = settings.ReadInt16("General", "RectangleAlpha", 200);

            //Reads the boolean for callout probability
            Globals.Application.CalloutProbability = settings.ReadBoolean("General", "CalloutProbability", true);

            //Reads the int for the callout probability modifier
            Globals.Application.CalloutProbabilityModifier = settings.ReadInt16("General", "CalloutProbabilityModifier", 1);

            //Reads the int for stopping the current callout first
            Globals.Application.StopCurrentCallout = settings.ReadBoolean("General", "StopCurrentCallout", true);

            //Makes a new converter to convert strings to keys
            KeysConverter kc = new KeysConverter();

            //Reads the key and modifier to force a callout from the ini file
            Globals.Controls.ForceCalloutKey = (Keys)kc.ConvertFromString(settings.ReadString("Keybindings", "ForceCalloutKey", "X"));
            Globals.Controls.ForceCalloutModifier = (Keys)kc.ConvertFromString(settings.ReadString("Keybindings", "ForceCalloutModifier", "None"));

            //Reads the key and modifier to set the player's availability from the ini file
            Globals.Controls.AvailabilityKey = (Keys)kc.ConvertFrom(settings.ReadString("Keybindings", "AvailabilityKey", "Z"));
            Globals.Controls.AvailabilityModifier = (Keys)kc.ConvertFromString(settings.ReadString("Keybindings", "AvailabilityModifier", "None"));

            //Logs some things
            Logger.Log("AvailableForCalloutsText is set to " + Globals.Application.AvailableForCalloutsText);
            Logger.Log("RectangleAlpha is set to " + Globals.Application.RectangleAlpha);
            Logger.Log("CalloutProbability is set to " + Globals.Application.CalloutProbability);
            Logger.Log("CalloutProbabilityModifier is set to " + Globals.Application.CalloutProbabilityModifier);
            Logger.Log("DebugLogging is set to " + Globals.Application.DebugLogging);
            Logger.Log("ForceCalloutKey is set to " + Globals.Controls.ForceCalloutKey);
            Logger.Log("ForceCalloutModifier is set to " + Globals.Controls.ForceCalloutModifier);
            Logger.Log("AvailabilityKey is set to " + Globals.Controls.AvailabilityKey);
            Logger.Log("AvailabilityModifier is set to " + Globals.Controls.AvailabilityModifier);

            Globals.Application.SettingsLoaded = true;
        }
    }
}
