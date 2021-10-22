using Agents;
using Enemies;
using Gear;
using Player;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace SilencedWeapons.SilencedWeaponsBase
{
    class SilencerNoiseHandler : MonoBehaviour
    {
        public SilencerNoiseHandler(IntPtr value) : base(value)
        { }



        public void Start()
        {
            SoftAlert_NoiseData = new NM_NoiseData()
            {
                radiusMin = 0.01f,
                radiusMax = 1f,
                yScale = 1f,
                type = NM_NoiseType.Detectable,
                includeToNeightbourAreas = true,
                raycastFirstNode = true
            };

            HardAlert_NoiseData = new NM_NoiseData()
            {
                radiusMin = 0.01f,
                radiusMax = 1f,
                yScale = 1f,
                type = NM_NoiseType.InstaDetect,
                includeToNeightbourAreas = true,
                raycastFirstNode = true
            };
        }

        public unsafe void Fire()
        {
            if (Time.time < cooldown || !SNet.IsMaster) return;
            cooldown = Time.time + 1f;

            Owner ??= gameObject.GetComponent<BulletWeapon>().Owner;
            var listeners = Physics.OverlapSphere(Owner.Position, MathF.Max(SoftAlert_Radius, HardAlert_Radius), LayerManager.MASK_ENEMY_DAMAGABLE);

            IDamageable enemyDamagable;
            EnemyAgent enemy;
            List<string> enemyMemory = new List<string>();

            foreach (var collider in listeners)
            {
                enemyDamagable = collider.GetComponent<IDamageable>();
                if (enemyDamagable == null) continue;

                enemy = enemyDamagable.TryCast<Dam_EnemyDamageLimb>().GlueTargetEnemyAgent;
                if (enemy == null || enemyMemory.Contains(enemy.gameObject.name)) continue;
                enemyMemory.Add(enemy.gameObject.name);

                if (Physics.Linecast(Owner.EyePosition, enemy.EyePosition, LayerManager.MASK_WORLD)) continue;
                if (Vector3.Distance(Owner.EyePosition, enemy.EyePosition) <= HardAlert_Radius)
                {
                    HardAlert_NoiseData.noiseMaker = Owner.Cast<INM_NoiseMaker>();
                    HardAlert_NoiseData.position = enemy.Position;
                    HardAlert_NoiseData.node = enemy.CourseNode;
                    NoiseManager.MakeNoise(HardAlert_NoiseData);
                }

                else
                {
                    SoftAlert_NoiseData.noiseMaker = Owner.Cast<INM_NoiseMaker>();
                    SoftAlert_NoiseData.position = enemy.Position;
                    SoftAlert_NoiseData.node = enemy.CourseNode;
                    NoiseManager.MakeNoise(SoftAlert_NoiseData);
                }
            }
        }

        public NM_NoiseData SoftAlert_NoiseData;
        public NM_NoiseData HardAlert_NoiseData;
        public float SoftAlert_Radius;
        public float HardAlert_Radius;
        public PlayerAgent Owner;

        public float cooldown = 0f;
    }
}
