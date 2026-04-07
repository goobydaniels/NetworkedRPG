using static Quantum.OverworldData;

namespace Quantum {
    public unsafe partial class Frame {
        public SpawnpointData GetRandomSpawnpoint() {
            OverworldData data = FindAsset<OverworldData>(Map.UserAsset);
            return data.spawnpoints[RNG->Next(0, data.spawnpoints.Length)];
        }

        public SpawnpointData GetSpawnpoint(int index) {
            OverworldData data = FindAsset<OverworldData>(Map.UserAsset);
            return data.spawnpoints[index];
        }

        public EntityRef GetPlayerEntity(PlayerRef playerRef) {
            foreach (EntityComponentPair<PlayerLink> pair in GetComponentIterator<PlayerLink>()) {
                if (pair.Component.Player == playerRef) return pair.Entity;
            }

            return EntityRef.None;
        }
#if UNITY_ENGINE

#endif
    }
}