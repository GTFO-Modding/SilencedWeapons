using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Agents;
using Gear;
using MTFO.Managers;
using Player;
using SilencedWeapons.SilencedWeaponsBase.Patches;
using UnityEngine;

namespace SilencedWeapons.SilencedWeaponsBase
{
    class SilencerManager
    {
        public static void Deserialize()
        {
            Log.Info($"{EntryPoint.NAME} version{EntryPoint.VERSION} by mccad00\nDeserializing custom silencer data");


            Filepath = Path.Combine(ConfigManager.CustomPath, "mccad00");
            if (!Directory.Exists(Filepath)) Directory.CreateDirectory(Filepath);


            Filepath = Path.Combine(Filepath, "Silencers.json");
            if (!File.Exists(Filepath))
            {
                Log.Info("First time setup: generating Silencers.json");
                Content = JsonSerializer.Serialize(new List<SilencerDatablock>() { new SilencerDatablock() }, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(Filepath, Content);
            }
            else
            {
                Content = File.ReadAllText(Filepath);
            }

            if (Content == null)
            {
                Log.Error("Error reading silencer datablock - Deserialized content is null!");
                return;
            }

            var silencerData = JsonSerializer.Deserialize<List<SilencerDatablock>>(Content);
            foreach (var entry in silencerData)
            {
                SilencerData.Add(entry.ArchetypeID, entry);
            }
        }

        public static bool TryApplySilencerData(ref BulletWeapon bulletWeapon, out SilencerNoiseHandler noiseHandler)
        {
            if (!SilencerData.TryGetValue(bulletWeapon.ArchetypeData.persistentID, out var silencerConfig) || !silencerConfig.InternalEnabled)
            {
                noiseHandler = null;
                return false;
            }
            Log.Info($"Applying silencer data {silencerConfig.InternalName} to weapon {bulletWeapon.ArchetypeData.PublicName} ID {silencerConfig.ArchetypeID}");
            noiseHandler = bulletWeapon.gameObject.AddComponent<SilencerNoiseHandler>();

            noiseHandler.SoftAlert_Radius = silencerConfig.SilencerSettingsData.Audio_SoftAlertRadius;
            noiseHandler.HardAlert_Radius = silencerConfig.SilencerSettingsData.Audio_HardAlertRadius;
            return true;
        }

        public static void BulletWeaponFire(IntPtr pointer, PlayerAgent owner)
        {
            if (!Patch_GearBuilder.SilencedWeapons.TryGetValue(pointer, out var noiseHandler)) return;
            owner.m_noise = Agent.NoiseType.Silent;
            noiseHandler.Fire();
        }

        public static string Filepath { get; set; }
        public static string Content { get; set; }
        public static Dictionary<uint, SilencerDatablock> SilencerData { get; set; } = new();
    }
}
