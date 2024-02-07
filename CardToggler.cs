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
        public const string pluginVersion = "0.1.0";

        internal static ManualLogSource Log;
        protected KeyboardShortcut kbAlpha1;
        protected KeyboardShortcut kbAlpha2;
        protected KeyboardShortcut kbAlpha3;
        protected KeyboardShortcut kbAlpha4;
        protected KeyboardShortcut kbAlpha5;
        protected KeyboardShortcut kbAlpha6;
        protected KeyboardShortcut kbAlpha7;
        protected KeyboardShortcut kbAlpha8;

        protected string[] weaponShortNames = {
            "KTNA"
            ,"PRFY"
            ,"ELVT"
            ,"GDSP"
            ,"STMP"
            ,"FRBL"
            ,"DOMN"
            ,"BOLF"
        };

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

            // Patching the Force Setup method (allowing level start w/ Fists)
            MethodInfo orgForceSetup = AccessTools.Method(typeof(MechController), "ForceSetup");
            MethodInfo newForceSetup = AccessTools.Method(typeof(TogglePatches), "ForceSetup_CTPatch");
            hrm.Patch(orgForceSetup, null, new HarmonyMethod(newForceSetup));

            // Notifying that all patches were completed successfully
            Logger.LogInfo("Mod done loading!");
        }

        public void Update()
        {
            // Katana Toggle
            if (kbAlpha1.IsDown())
            {
                TogglePatches.EnabledWeapons[0] = !TogglePatches.EnabledWeapons[0];
                PrintPickupStatus(0);
            }
            // Purify Toggle
            else if (kbAlpha2.IsDown())
            {
                TogglePatches.EnabledWeapons[1] = !TogglePatches.EnabledWeapons[1];
                PrintPickupStatus(1);
            }
            // Elevate Toggle
            else if (kbAlpha3.IsDown())
            {
                TogglePatches.EnabledWeapons[2] = !TogglePatches.EnabledWeapons[2];
                PrintPickupStatus(2);
            }
            // Godspeed Toggle
            else if (kbAlpha4.IsDown())
            {
                TogglePatches.EnabledWeapons[3] = !TogglePatches.EnabledWeapons[3];
                PrintPickupStatus(3);
            }
            // Stomp Toggle
            else if (kbAlpha5.IsDown())
            {
                TogglePatches.EnabledWeapons[4] = !TogglePatches.EnabledWeapons[4];
                PrintPickupStatus(4);
            }
            // Fireball Toggle
            else if (kbAlpha6.IsDown())
            {
                TogglePatches.EnabledWeapons[5] = !TogglePatches.EnabledWeapons[5];
                PrintPickupStatus(5);
            }
            // Dominion Toggle
            else if (kbAlpha7.IsDown())
            {
                TogglePatches.EnabledWeapons[6] = !TogglePatches.EnabledWeapons[6];
                PrintPickupStatus(6);
            }
            // Book of Life Toggle
            else if (kbAlpha8.IsDown())
            {
                TogglePatches.EnabledWeapons[7] = !TogglePatches.EnabledWeapons[7];
                PrintPickupStatus(7);
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
            for (int num = 0; num < weaponShortNames.Length; num++)
            {
                status += "\n" + (numPressed == num ? "> " : "  ");
                status += weaponShortNames[num] + ": " + TogglePatches.EnabledWeapons[num].ToString();
                status += (numPressed == num ? " <" : "  ");
            }
            status += "\n\n";

            Logger.LogInfo(status);
        }
    }
}
