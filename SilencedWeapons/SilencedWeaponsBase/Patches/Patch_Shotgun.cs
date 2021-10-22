using Agents;
using Gear;
using HarmonyLib;
using Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace SilencedWeapons.SilencedWeaponsBase.Patches
{
    [HarmonyPatch]
    class Patch_Shotgun
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Shotgun), nameof(Shotgun.Fire))]
        public static void Fire(Shotgun __instance)
        {
            SilencerManager.BulletWeaponFire(__instance.Pointer, __instance.Owner);
        }
    }

    
}
