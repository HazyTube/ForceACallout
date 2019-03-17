/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to NoNameSet for helping with the on screen text box

*/

using System.Reflection;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForceACallout.Utils
{
    public static class RandomCallouts
    {
        internal static List<string> RandomCalloutCache = new List<string>();

        static RandomCallouts()
        {
            CacheCallouts();
        }

        internal static void CacheCallouts()
        {
            foreach (Assembly Assem in Functions.GetAllUserPlugins())
            {
                AssemblyName AssemName = Assem.GetName();
                List<Type> AssemCallouts = (from Callout in Assem.GetTypes()
                                            where Callout.IsClass && Callout.BaseType == typeof(LSPD_First_Response.Mod.Callouts.Callout)
                                            select Callout).ToList();

                if (AssemCallouts.Count() < 1)
                {
                    Logger.Log(Assem.FullName + "No callouts detected.");
                }

                else
                {
                    int AddCount = 0;
                    foreach (Type Callout in AssemCallouts)
                    {
                        object[] CalloutAttributes = Callout.GetCustomAttributes(typeof(CalloutInfoAttribute), true);

                        if (CalloutAttributes.Count() > 0)
                        {
                            CalloutInfoAttribute CalloutAttribute = (CalloutInfoAttribute)(from a in CalloutAttributes select a).FirstOrDefault();

                            if (CalloutAttribute != null)
                            {
                                RandomCalloutCache.Add(CalloutAttribute.Name);
                                AddCount++;
                            }
                        }
                    }

                    Logger.Log(Assem.FullName + $" detected {AddCount} callouts and added them to the ForceACallout cache.");
                }
            }
        }

        internal static string StartRandomCallout()
        {
            Logger.DebugLog("StartRandomCallout() Started");
            Random RandomValue = new Random();

            try
            {
                string RandomCallout = RandomCalloutCache[RandomValue.Next(0, RandomCalloutCache.Count)];

                Functions.StartCallout(RandomCallout);
                return RandomCallout;
            }

            catch
            {
                Logger.Log("Could not start callout, see RagePluginHook.log for more details");
                return null;
            }
        }
    }
}
