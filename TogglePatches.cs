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
        public static bool[] EnabledWeapons = { true, true, true, true, true, true, true, true };

        public static bool OnPickupCard_CTPatch(PlayerCardData card, int overrideAmmo = -1)
        {
            bool canPickup = true;

            switch (card.cardID)
            {
                case "MACHINEGUN":
                    canPickup = EnabledWeapons[1];
                    break;
                case "PISTOL":
                    canPickup = EnabledWeapons[2];
                    break;
                case "RIFLE":
                    canPickup = EnabledWeapons[3];
                    break;
                case "UZI":
                    canPickup = EnabledWeapons[4];
                    break;
                case "SHOTGUN":
                    canPickup = EnabledWeapons[5];
                    break;
                case "ROCKETLAUNCHER":
                    canPickup = EnabledWeapons[6];
                    break;
                case "RAPTURE":
                    canPickup = EnabledWeapons[7];
                    break;
                default:
                    break;
            }

            return canPickup;
        }

        public static void ForceSetup_CTPatch()
        {
            if (!EnabledWeapons[0])
            {
                PlayerCardData fistsCard = Singleton<Game>.Instance.GetGameData().GetCard("FISTS");
                RM.mechController.OnPickupCard(fistsCard, -1);
            }
        }
    }
}
