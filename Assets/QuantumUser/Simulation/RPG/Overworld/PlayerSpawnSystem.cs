using UnityEngine;

namespace Quantum {
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime) {
            {
                RuntimePlayer data = f.GetPlayerData(player);
                EntityPrototype asset = f.FindAsset<EntityPrototype>(data.PlayerAvatar);
                EntityRef entityRef = f.Create(asset);
                OverworldData overworld = f.FindAsset<OverworldData>(f.Map.UserAsset);

                f.Add(entityRef, new PlayerLink { Player = player });

                overworld.SetEntityToSpawnpoint(f, entityRef, player._index - 1);
            }
        }
    }
}
