namespace Quantum {
    using Photon.Deterministic;
    using System;
    using UnityEngine;

    public unsafe class PlayerSystem : SystemMainThreadFilter<PlayerSystem.Filter>, ISignalOnComponentAdded<PlayerLink> {
        [Serializable]
        public struct Filter {
            public EntityRef Entity;
            public PlayerLink* PlayerLink;
            public KCC* KCC;
            public Transform3D* Transform;
        }

        public unsafe void OnAdded(Frame f, EntityRef entity, PlayerLink* player) {
            player->BaseSpeed = 10;
            player->CurrentSpeed = player->BaseSpeed;
        }

        public override void Update(Frame frame, ref Filter filter) {
            PlayerLink* playerLink = filter.PlayerLink;
            
            if (!playerLink->Player.IsValid) return;

            //playerLink->CurrentPos = filter.Transform->Position; 

            KCC* kcc = filter.KCC;
            Input* input = frame.GetPlayerInput(playerLink->Player);

            kcc->AddLookRotation(input->LookRotationDelta.X, input->LookRotationDelta.Y);
            kcc->SetInputDirection(kcc->Data.TransformRotation * input->Direction.XOY);

            //EnvironmentProcessor.SetGravity(new FPVector3(0, -20, 0));
            HandleNormalMovement(kcc, input, ref filter);

            if (input->Jump.WasPressed && kcc->IsGrounded) {
                kcc->Jump(FPVector3.Up * playerLink->JumpForce);
            }
        }

        private void HandleNormalMovement(KCC* kcc, Input* input, ref Filter filter) {
            FPVector3 dir = kcc->Data.TransformRotation * input->Direction.XOY;
            kcc->SetInputDirection(dir * filter.PlayerLink->CurrentSpeed);
        }
    }
}
