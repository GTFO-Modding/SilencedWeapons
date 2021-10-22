using System;
using System.Collections.Generic;
using System.Text;

namespace SilencedWeapons.SilencedWeaponsBase
{
    public class SilencerDatablock
    {
        public uint ArchetypeID { get; set; } = 0;
        public string InternalName { get; set; } = "Internal name - not referenced ingame";
        public bool InternalEnabled { get; set; } = false;
        public SilencerSettings SilencerSettingsData { get; set; } = new();
    }

    public class SilencerSettings
    {
        public float Audio_SoftAlertRadius { get; set; } = 0;
        public float Audio_HardAlertRadius { get; set; } = 0;
    }
}
