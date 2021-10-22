using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using SilencedWeapons.SilencedWeaponsBase;
using SilencedWeapons.SilencedWeaponsBase.Patches;
using UnhollowerRuntimeLib;

namespace SilencedWeapons
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class EntryPoint : BasePlugin
    {
        // The method that gets called when BepInEx tries to load our plugin
        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<SilencerNoiseHandler>();
            m_Harmony.PatchAll();
        }

        public static Harmony m_Harmony = new Harmony(GUID);

        public const string VERSION = "1.0.2";
        public const string NAME = "SilencedWeapons";
        public const string GUID = "com.mccad00.SilencedWeapons";
    }
}
