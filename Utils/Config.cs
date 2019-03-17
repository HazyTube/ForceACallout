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
            InitializationFile settings = initialiseFile(Globals.Application.ConfigPath + "ForceACallout.ini");

            KeysConverter kc = new KeysConverter();

            string opTemp;
            string temp2;
            string temp3;

            opTemp = settings.ReadString("Keybindings", "ForceCalloutKey", "X");
            temp2 = settings.ReadString("General", "DebugLogging", "False");
            temp3 = settings.ReadString("Keybindings", "AvailabilityKey", "Z");

            Logger.Log("ForceCalloutKey is set to " + opTemp);
            Logger.Log("DebugLogging is set to " + temp2);
            Logger.Log("AvailabilityKey is set to " + temp3);

            Globals.Controls.ForceCalloutKey = (Keys)kc.ConvertFromString(opTemp);
            Globals.Controls.AvailabilityKey = (Keys)kc.ConvertFromString(temp3);
            Globals.Application.DebugLogging = Convert.ToBoolean(temp2);

        }
    }
}
