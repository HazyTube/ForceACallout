/*

Developed By: HazyTube
Name: Force A Callout
Released on: GitHub and LSPDFR

Credits:
Thanks to LiverLande for this gist: https://gist.githubusercontent.com/RiverGrande/d27b7506d5eb1372e53f1840a8a647c8/raw/a71c93eb007f9b35e3b1e376026624507779f40e/RandomCallouts.cs
Thanks to LiverLande for this amazing pull request: https://github.com/HazyTube/ForceACallout/pull/2
Thanks to NoNameSet for helping with the on screen text box

*/

using System.Reflection;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System;
using System.Collections.Generic;
using System.Linq;
using Rage;

namespace ForceACallout.Utils
{
    internal static class RandomCallouts
    {
        private static readonly List<string> RandomCalloutCache = new List<string>();
        private static readonly int[] CalloutProbabilityRegistrationCount =
        {
            95, 80, 70, 50, 30, 15, 0
        };

        static RandomCallouts()
        {
            GameFiber.StartNew(delegate
            {
                GameFiber.WaitWhile(() => !Globals.Application.SettingsLoaded);
                CacheCallouts();
            }, "CalloutCacheLoader");
        }

        private static void CacheCallouts()
        {
            foreach (var Assem in Functions.GetAllUserPlugins())
            {
                var AssemName = Assem.GetName();
                var AssemCallouts = (from Callout in Assem.GetTypes()
                                            where Callout.IsClass && Callout.BaseType == typeof(LSPD_First_Response.Mod.Callouts.Callout)
                                            select Callout).ToList();

                if (AssemCallouts.Count() < 1)
                {
                    Logger.Log(Assem.GetName().Name + " No callouts detected.");
                }

                else
                {
                    var AddCount = 0;
                    foreach (Type Callout in AssemCallouts)
                    {
                        var CalloutAttributes = Callout.GetCustomAttributes(typeof(CalloutInfoAttribute), true);

                        if (CalloutAttributes.Count() > 0)
                        {
                            var CalloutAttribute = (CalloutInfoAttribute)(from a in CalloutAttributes select a).FirstOrDefault();

                            if (CalloutAttribute != null)
                            {
                                if (Globals.Config.CalloutProbability == false)
                                    RandomCalloutCache.Add(CalloutAttribute.Name);

                                else
                                {
                                    for (var LoopCount = 0; LoopCount < CalloutProbabilityRegistrationCount[(int)CalloutAttribute.CalloutProbability] * Globals.Config.CalloutProbabilityModifier; LoopCount++)
                                    {
                                        RandomCalloutCache.Add(CalloutAttribute.Name);
                                    }
                                }
                                AddCount++;
                            }
                        }
                    }

                    Logger.Log(Assem.GetName().Name + $" detected {AddCount} callouts and added them to the ForceACallout cache.");
                }
            }

            if (Globals.Config.CalloutProbability == true)
            {
                Logger.Log($"{RandomCalloutCache.Count} total probabilities registered in ForceACallout.");
            }
        }

        internal static string StartRandomCallout()
        {
            Logger.DebugLog("StartRandomCallout() Started");
            var RandomValue = new Random();

            try
            {
                var RandomCallout = RandomCalloutCache[RandomValue.Next(0, RandomCalloutCache.Count)];

                Functions.StartCallout(RandomCallout);
                Logger.Log($"Starting callout {RandomCallout}");
                return RandomCallout;
            }

            catch
            {
                Logger.Log("Could not start callout!");
                return null;
            }
        }
    }
}
