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
    class Patch_ShotgunSynced
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ShotgunSynced), nameof(ShotgunSynced.Fire))]
        public static void Fire(ShotgunSynced __instance)
        {
            SilencerManager.BulletWeaponFire(__instance.Pointer, __instance.Owner);
        }
    }

    
}
