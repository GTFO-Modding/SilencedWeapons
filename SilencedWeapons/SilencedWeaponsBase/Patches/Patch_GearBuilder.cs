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
    class Patch_GearBuilder
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GearBuilder), nameof(GearBuilder.AssembleGearAsync))]

        public static void AssembleGearAsync(ItemEquippable __result)
        {
            if (__result == null) return;

            var bulletWeapon = __result.gameObject.GetComponent<BulletWeapon>();
            if (bulletWeapon == null) return;

            if (!SilencerManager.TryApplySilencerData(ref bulletWeapon, out var noiseHandler)) return;
            if (!SilencedWeapons.ContainsKey(bulletWeapon.Pointer)) SilencedWeapons.Add(bulletWeapon.Pointer, noiseHandler);
        }

        public static PlayerAgent Owner { get; set; } = null;
        public static bool DoSilence { get; set; } = false;
        public static Dictionary<IntPtr, SilencerNoiseHandler> SilencedWeapons { get; set; } = new();
    }


}
