using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonWhite_CardToggler
{
    public class TogglePatches
    {
        public static bool KatanaEnabled = true;
        public static bool PurifyEnabled = true;
        public static bool ElevateEnabled = true;
        public static bool GodspeedEnabled = true;
        public static bool StompEnabled = true;
        public static bool FireballEnabled = true;
        public static bool DominionEnabled = true;
        public static bool BookOfLifeEnabled = true;

        public static bool OnPickupCard_CTPatch(PlayerCardData card, int overrideAmmo = -1)
        {
            bool canPickup = true;

            switch (card.cardID)
            {
                case "MACHINEGUN":
                    canPickup = PurifyEnabled;
                    break;
                case "PISTOL":
                    canPickup = ElevateEnabled;
                    break;
                case "RIFLE":
                    canPickup = GodspeedEnabled;
                    break;
                case "UZI":
                    canPickup = StompEnabled;
                    break;
                case "SHOTGUN":
                    canPickup = FireballEnabled;
                    break;
                case "ROCKETLAUNCHER":
                    canPickup = DominionEnabled;
                    break;
                case "RAPTURE":
                    canPickup = BookOfLifeEnabled;
                    break;
                default:
                    break;
            }

            return canPickup;
        }

        public static void PlayLevel_CTPatch(LevelData newLevel, bool fromArchive, bool fromRestart = false)
        {
            if (!KatanaEnabled)
                modifySidearm("FISTS");
            else
                modifySidearm("KATANA");
        }

        public static void PlayLevel_CTPatch(string newLevelID, bool fromArchive, Action onLevelLoadComplete = null)
        {
            if (!KatanaEnabled)
                modifySidearm("FISTS");
            else
                modifySidearm("KATANA");
        }

        public static void modifySidearm(string sidearm)
        {
            Singleton<MechController>.Instance.ChangeSidearm(sidearm, false);
        }
    }
}
