namespace Quantum {
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class OverworldSystem : SystemSignalsOnly, ISignalOnPlayerAdded, ISignalOnPlayerRemoved {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime) {
            RuntimePlayer data = f.GetPlayerData(player);
            EntityRef entityRef = f.Create(data.PlayerAvatar);
            OverworldData overworld = f.FindAsset<OverworldData>(f.Map.UserAsset);

            PlayerLink* playerLink = f.Unsafe.GetPointer<PlayerLink>(entityRef);
            playerLink->Player = player;

            overworld.SetEntityToSpawnpoint(f, entityRef, player._index - 1);
            KCC* playerKCC = f.Unsafe.GetPointer<KCC>(entityRef);
            playerKCC->SetLookRotation(f.GetSpawnpoint(player._index - 1).rotation.AsEuler.XY);
        }

        public void OnPlayerRemoved(Frame f, PlayerRef player) {
            EntityRef entityRef = f.GetPlayerEntity(player);
            if (entityRef.IsValid) {
                f.Destroy(entityRef);
            }
        }
    }
}
