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
    class Patch_BulletWeapon
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BulletWeapon), nameof(BulletWeapon.Fire))]
        public static void Fire(BulletWeapon __instance)
        {
            SilencerManager.BulletWeaponFire(__instance.Pointer, __instance.Owner);
        }
    }

    
}
