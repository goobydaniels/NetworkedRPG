using UnityEngine;

namespace Quantum {
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime) {
            {
                if (!firstTime) return;

                EntityRef entityRef = CreatePlayer(f, player);
                
                //data.location = data.GetSpawnPos();

                //// Setting the player's position
                //if (f.Unsafe.TryGetPointer<Transform3D>(entityRef, out var transform)) {
                //    transform->Position = data.location;
                //}
            }
        }

        private EntityRef CreatePlayer(Frame f, PlayerRef player) {
            RuntimePlayer rtPlayer = f.GetPlayerData(player);
            EntityRef entityRef = f.Create(rtPlayer.PlayerAvatar);

            PlayerLink link = new PlayerLink {
                Player = player
            };

            f.Add(entityRef, link);
            return entityRef;
        }
    }
}
