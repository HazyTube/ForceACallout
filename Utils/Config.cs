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

            //Makes a new converter to convert strings to keys
            KeysConverter kc = new KeysConverter();

            string FoceCalloutKey;
            string ForceCalloutModifier;
            string DebugLogging;
            string AvailabilityKey;
            string AvailabilityModifier;
            string AvailableForCalloutsText;
            int RectangleAlpha;

            //Reads the key and modifier to force a callout from the ini file
            FoceCalloutKey = settings.ReadString("Keybindings", "ForceCalloutKey", "X");
            ForceCalloutModifier = settings.ReadString("Keybindings", "ForceCalloutModifier", "None");

            //Reads the value for debug logging from the ini file
            DebugLogging = settings.ReadString("General", "DebugLogging", "False");

            //Reads the key and modifier to set the player's availability from the ini file
            AvailabilityKey = settings.ReadString("Keybindings", "AvailabilityKey", "Z");
            AvailabilityModifier = settings.ReadString("Keybindings", "AvailabilityModifier", "None");

            //Reads the value to enable or disable the on screen text
            AvailableForCalloutsText = settings.ReadString("General", "AvailableForCalloutsText", "True");

            //Reads the int for the rectangle alpha
            RectangleAlpha = settings.ReadInt16("General", "RectangleAlpha", 200);

            //Logs some things
            Logger.Log("DebugLogging is set to " + DebugLogging);
            Logger.Log("ForceCalloutKey is set to " + FoceCalloutKey);
            Logger.Log("ForceCalloutModifier is set to " + ForceCalloutModifier);
            Logger.Log("AvailabilityKey is set to " + AvailabilityKey);
            Logger.Log("AvailabilityModifier is set to " + AvailabilityModifier);
            Logger.Log("AvailableForCalloutsText is set to " + AvailableForCalloutsText);
            Logger.Log("RectangleAlpha is set to " + RectangleAlpha);

            //These convert strings to keys
            Globals.Controls.ForceCalloutKey = (Keys)kc.ConvertFromString(FoceCalloutKey);
            Globals.Controls.ForceCalloutModifier = (Keys)kc.ConvertFromString(ForceCalloutModifier);

            Globals.Controls.AvailabilityKey = (Keys)kc.ConvertFromString(AvailabilityKey);
            Globals.Controls.AvailabilityModifier = (Keys)kc.ConvertFromString(AvailabilityModifier);

            //Converts a string to a bool
            Globals.Application.DebugLogging = Convert.ToBoolean(DebugLogging);

            //Converts a string to a bool
            Globals.Application.AvailableForCalloutsText = Convert.ToBoolean(AvailableForCalloutsText);

            //Assigns the rectangle alpha int to something
            Globals.Application.RectangleAlpha = RectangleAlpha;
        }
    }
}
