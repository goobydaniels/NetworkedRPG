namespace Quantum {
    using System;
    using Photon.Deterministic;

    public unsafe class OverworldData : AssetObject {
        [Serializable]
        public struct SpawnPointData {
            public FPVector3 position;
            public FPQuaternion rotation;
        }

        public SpawnPointData DefaultSpawnPoint;
        public SpawnPointData[] spawnPoints;

        public void SetEntityToSpawnPoint(Frame frame, EntityRef entity, Int32? index) {
            var transform = frame.Unsafe.GetPointer<Transform3D>(entity);
            var spawnPoint = index.HasValue && index.Value < spawnPoints.Length ? spawnPoints[index.Value] : DefaultSpawnPoint;
            transform->Position = spawnPoint.position;
            transform->Rotation = spawnPoint.rotation;
        }
    }
}