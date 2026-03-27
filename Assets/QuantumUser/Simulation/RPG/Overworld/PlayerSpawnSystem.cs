using UnityEngine;

namespace Quantum {
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime) {
            {
                RuntimePlayer data = f.GetPlayerData(player);
                EntityPrototype asset = f.FindAsset<EntityPrototype>(data.PlayerAvatar);
                EntityRef entityRef = f.Create(asset);
                f.Add(entityRef, new PlayerLink { Player = player });
                data.location = data.GetSpawnPos();
                Debug.Log(data.location);
            }
        }
    }
}
