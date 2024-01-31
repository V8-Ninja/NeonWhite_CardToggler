using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NeonWhite_CardToggler
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class CardToggler : BaseUnityPlugin
    {
        public const string pluginGuid = "v8ninja.neonwhite.cardtoggler";
        public const string pluginName = "Card Toggler (by V8_Ninja)";
        public const string pluginVersion = "0.0.1";

        internal static ManualLogSource Log;
        protected KeyboardShortcut kbAlpha1;
        protected KeyboardShortcut kbAlpha2;
        protected KeyboardShortcut kbAlpha3;
        protected KeyboardShortcut kbAlpha4;
        protected KeyboardShortcut kbAlpha5;
        protected KeyboardShortcut kbAlpha6;
        protected KeyboardShortcut kbAlpha7;
        protected KeyboardShortcut kbAlpha8;

        public void Start()
        {
            // Setting up the static logger
            Log = this.Logger;

            // Creating the keyboard shortcuts
            kbAlpha1 = new KeyboardShortcut(KeyCode.Alpha1);
            kbAlpha2 = new KeyboardShortcut(KeyCode.Alpha2);
            kbAlpha3 = new KeyboardShortcut(KeyCode.Alpha3);
            kbAlpha4 = new KeyboardShortcut(KeyCode.Alpha4);
            kbAlpha5 = new KeyboardShortcut(KeyCode.Alpha5);
            kbAlpha6 = new KeyboardShortcut(KeyCode.Alpha6);
            kbAlpha7 = new KeyboardShortcut(KeyCode.Alpha7);
            kbAlpha8 = new KeyboardShortcut(KeyCode.Alpha8);

            // Patching the "On Pickup Card" method
            Harmony hrm = new Harmony(pluginGuid);

            MethodInfo orgOnPickupCard = AccessTools.Method(typeof(MechController), "OnPickupCard");
            MethodInfo newOnPickupCard = AccessTools.Method(typeof(TogglePatches), "OnPickupCard_CTPatch");
            hrm.Patch(orgOnPickupCard, new HarmonyMethod(newOnPickupCard));

            // Patching the various "Play Level" methods
            MethodInfo orgPlayLevel1 = AccessTools.Method(
                typeof(Game)
                ,"PlayLevel"
                ,new[] {
                    typeof(LevelData)
                    ,typeof(bool)
                    ,typeof(bool)
                }
            );
            MethodInfo newPlayLevel1 = AccessTools.Method(
                typeof(TogglePatches)
                , "PlayLevel_CTPatch"
                , new[] {
                    typeof(LevelData)
                    ,typeof(bool)
                    ,typeof(bool)
                }
            );
            hrm.Patch(orgPlayLevel1, null, new HarmonyMethod(newPlayLevel1));

            /*
            MethodInfo orgPlayLevel2 = AccessTools.Method(
                typeof(Game)
                ,"PlayLevel"
                ,new[] {
                    typeof(string)
                    ,typeof(bool)
                    ,typeof(Action)
                }
            );
            MethodInfo newPlayLevel2 = AccessTools.Method(
                typeof(Game)
                ,"PlayLevel"
                , new[] {
                    typeof(string)
                    ,typeof(bool)
                    ,typeof(Action)
                }
            );
            hrm.Patch(orgPlayLevel2, null, new HarmonyMethod(newPlayLevel2));
            */

            // Notifying that all patches were completed successfully
            Logger.LogInfo("Mod done loading!");
        }

        public void Update()
        {
            if (kbAlpha1.IsDown())
            {
                TogglePatches.KatanaEnabled = !TogglePatches.KatanaEnabled;
                PrintPickupStatus(1);
            }
            else if (kbAlpha2.IsDown())
            {
                TogglePatches.PurifyEnabled = !TogglePatches.PurifyEnabled;
                PrintPickupStatus(2);
            }
            else if (kbAlpha3.IsDown())
            {
                TogglePatches.ElevateEnabled = !TogglePatches.ElevateEnabled;
                PrintPickupStatus(3);
            }
            else if (kbAlpha4.IsDown())
            {
                TogglePatches.GodspeedEnabled = !TogglePatches.GodspeedEnabled;
                PrintPickupStatus(4);
            }
            else if (kbAlpha5.IsDown())
            {
                TogglePatches.StompEnabled = !TogglePatches.StompEnabled;
                PrintPickupStatus(5);
            }
            else if (kbAlpha6.IsDown())
            {
                TogglePatches.FireballEnabled = !TogglePatches.FireballEnabled;
                PrintPickupStatus(6);
            }
            else if (kbAlpha7.IsDown())
            {
                TogglePatches.DominionEnabled = !TogglePatches.DominionEnabled;
                PrintPickupStatus(7);
            }
            else if (kbAlpha8.IsDown())
            {
                TogglePatches.BookOfLifeEnabled = !TogglePatches.BookOfLifeEnabled;
                PrintPickupStatus(8);
            }
        }

        public void OnDestroy()
        {
            Logger.LogInfo("Unpatching all methods...");
            Harmony hrm = new Harmony(pluginGuid);
            hrm.UnpatchSelf();
        }

        protected void PrintPickupStatus(int numPressed)
        {
            string status = "\n";
            status += "\n" + (numPressed == 1 ? "--> " : "") + "KTNA: " + TogglePatches.KatanaEnabled.ToString();
            status += "\n" + (numPressed == 2 ? "--> " : "") + "PRFY: " + TogglePatches.PurifyEnabled.ToString();
            status += "\n" + (numPressed == 3 ? "--> " : "") + "ELVT: " + TogglePatches.ElevateEnabled.ToString();
            status += "\n" + (numPressed == 4 ? "--> " : "") + "GDSP: " + TogglePatches.GodspeedEnabled.ToString();
            status += "\n" + (numPressed == 5 ? "--> " : "") + "STMP: " + TogglePatches.StompEnabled.ToString();
            status += "\n" + (numPressed == 6 ? "--> " : "") + "FRBL: " + TogglePatches.FireballEnabled.ToString();
            status += "\n" + (numPressed == 7 ? "--> " : "") + "DOMN: " + TogglePatches.DominionEnabled.ToString();
            status += "\n" + (numPressed == 8 ? "--> " : "") + "BOLF: " + TogglePatches.BookOfLifeEnabled.ToString();
            status += "\n\n";

            Logger.LogInfo(status);
        }
    }
}
