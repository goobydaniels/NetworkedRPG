namespace Quantum {
    using Photon.Deterministic;

    public unsafe class PlayerSystem : SystemMainThreadFilter<PlayerSystem.Filter> {
        public override void Update(Frame frame, ref Filter filter) {
            Input* input = frame.GetPlayerInput(filter.PlayerLink->Player);
            filter.PhysicsBody->AddForce(input->Direction * 10);
        }

        public struct Filter {
            public EntityRef Entity;
            public PlayerLink* PlayerLink;
            public PhysicsBody3D* PhysicsBody;
        }

        public void OnPlayerAdded(Frame f, PlayerRef playerRef, bool firstTime) {
            RuntimePlayer playerData = f.GetPlayerData(playerRef);
            EntityRef entityRef = f.Create(playerData.PlayerAvatar);
            PlayerLink* link = f.Unsafe.GetPointer<PlayerLink>(entityRef);
            link->Player = playerRef;
        }
    }
}
