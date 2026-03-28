using UnityEngine;
using Photon.Deterministic;

namespace Quantum.Battle
{
    public unsafe class EnemyWaveSpawner : SystemSignalsOnly
    {
        public override void OnInit(Frame frame)
        {
            SpawnEnemyWave(frame);
        }

        public void SpawnEnemy(Frame frame, AssetRef<EntityPrototype> childPrototype)
        {
            BattleGameConfig config = frame.FindAsset(frame.RuntimeConfig.GameConfig);

            EntityRef enemy = frame.Create(childPrototype);
            Transform3D* enemyTransform = frame.Unsafe.GetPointer<Transform3D>(enemy);

            enemyTransform->Position = SetBattlePosition(frame);
            enemyTransform->Rotation = SetBattleRotation(frame);

            if (frame.Unsafe.TryGetPointer<PhysicsBody3D>(enemy, out var body))
            {
                // Gets the physics body of the enemy
                body->Velocity = enemyTransform->Up;
            }
        }

        private void SpawnEnemyWave(Frame frame)
        {
            // Spawns number of enemies based on how many waves have spawned and increments the counter
            BattleGameConfig config = frame.FindAsset(frame.RuntimeConfig.GameConfig);

            for (int i = 0; i < frame.Global->BattleWaveCount + config.numOfWaves; i++)
            {
                SpawnEnemy(frame, config.enemyPrototype);
            }

            frame.Global->BattleWaveCount++;
        }

        public static FPVector3 SetBattlePosition(Frame frame)
        {
            return FPVector3.Zero;
        }

        public static FPQuaternion SetBattleRotation(Frame frame)
        {
            // return frame.RNG->Next(0, 10);
            var x = frame.RNG->Next(0, 360);
            var y = frame.RNG->Next(0, 360);
            var z = frame.RNG->Next(0, 360);

            // Create a random quaternion
            FPQuaternion randomRotation = FPQuaternion.CreateFromYawPitchRoll(x, y, z);

            return randomRotation;
        }
    
    }
}
