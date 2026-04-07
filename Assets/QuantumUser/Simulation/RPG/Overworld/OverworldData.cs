using Photon.Deterministic;
using System;

namespace Quantum {

    public unsafe class OverworldData : AssetObject {
        [Serializable]
        public struct SpawnpointData {
            public FPVector3 position;
            public FPQuaternion rotation;
        }

        public SpawnpointData defaultSpawnpoint;
        public SpawnpointData[] spawnpoints;

        public void SetEntityToSpawnpoint(Frame f, EntityRef entityRef, Int32? index) {
            Transform3D* transform = f.Unsafe.GetPointer<Transform3D>(entityRef);
            SpawnpointData spawnPoint = index.HasValue && index.Value < spawnpoints.Length
                ? spawnpoints[index.Value]
                : defaultSpawnpoint;

            transform->Position = spawnPoint.position;
            transform->Rotation = spawnPoint.rotation;
        }
    }
}
