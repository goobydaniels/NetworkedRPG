using UnityEngine;

namespace Quantum.Battle
{
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        public void OnPlayerAdded(Frame frame, PlayerRef player, bool firstTime)
        {
            {
                RuntimePlayer data = frame.GetPlayerData(player);

                // Resolve avatar prototype reference
                var entityPrototypAsset = frame.FindAsset<EntityPrototype>(data.PlayerAvatar);

                // Create new entity for player based on prototype
                var playerEntity = frame.Create(entityPrototypAsset);

                // Create a PlayerLink component
                frame.Add(playerEntity, new PlayerLink { Player = player });
            }
        }
    }
}
