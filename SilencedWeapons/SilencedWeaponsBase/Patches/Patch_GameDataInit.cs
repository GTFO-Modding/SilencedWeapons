using Agents;
using GameData;
using Gear;
using HarmonyLib;
using Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerRuntimeLib;

namespace SilencedWeapons.SilencedWeaponsBase.Patches
{
    [HarmonyPatch]
    class Patch_GameDataInit
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameDataInit), nameof(GameDataInit.Initialize))]
        public static void Initialize()
        {
            SilencerManager.Deserialize();
        }
    }


}
