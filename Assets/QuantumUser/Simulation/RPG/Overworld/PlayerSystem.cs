namespace Quantum {
    using Photon.Deterministic;
    using System;

    public unsafe class PlayerSystem : SystemMainThreadFilter<PlayerSystem.Filter> {
        [Serializable]
        public struct Filter {
            public EntityRef Entity;
            public PlayerLink* PlayerLink;
            public KCC* KCC;
        }

        public override void Update(Frame frame, ref Filter filter) {
            PlayerLink* playerLink = filter.PlayerLink;
            
            if (!playerLink->Player.IsValid) return;

            KCC* kcc = filter.KCC;
            Input* input = frame.GetPlayerInput(playerLink->Player);

            kcc->AddLookRotation(input->LookRotationDelta.X, input->LookRotationDelta.Y);
            kcc->SetInputDirection(kcc->Data.TransformRotation * input->Direction.XOY);

            if (input->Action.WasPressed && kcc->IsGrounded) {
                kcc->Jump(FPVector3.Up * playerLink->JumpForce);
            } 
        }

        public void OnPlayerAdded(Frame f, PlayerRef playerRef, bool firstTime) {
            RuntimePlayer playerData = f.GetPlayerData(playerRef);
            EntityRef entityRef = f.Create(playerData.PlayerAvatar);
            PlayerLink* link = f.Unsafe.GetPointer<PlayerLink>(entityRef);
            link->Player = playerRef;
        }
    }
}
