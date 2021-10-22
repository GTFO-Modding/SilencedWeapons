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
    class Patch_BulletWeaponSynced
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BulletWeaponSynced), nameof(BulletWeaponSynced.Fire))]
        public static void Fire(BulletWeaponSynced __instance)
        {
            SilencerManager.BulletWeaponFire(__instance.Pointer, __instance.Owner);
        }
    }
}
