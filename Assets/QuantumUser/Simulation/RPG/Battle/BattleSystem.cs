using UnityEngine;

// namespace is optional, if not using the namespace add "using Quantum"
namespace Quantum.Battle
{
    // Unsafe allows pointers
    public unsafe class BattleSystem : SystemMainThreadFilter<BattleSystem.Filter>
    {
        // Each filter should always contain an EntityRef
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PhysicsBody3D* body3D;
            public Player* player;
        }

        // Override update function. the update function runs each frame for each entity that has all the componets in the filter
        public override void Update(Frame frame, ref Filter filter)
        {
            // Gets the input for player 0
            var input = frame.GetPlayerInput(0);

            UpdateMovement(frame, ref filter, input);

            // filter.body3D->AddForce(filter.Transform->Up);
        }

        // Input is sent every tick and is used for inputs that change frequently and affect the real time gameplay. Examples are movement and button presses. For irregular rare inputs, use commands instead.
        private void UpdateMovement(Frame frame, ref Filter filter, Input* input)
        {
            if (input->MenuLeft)
            {

            }

            if (input->MenuRight)
            {

            }

            if (input->MenuUp)
            {
                // Menu up behavior
                // filter.body3D->AddLinearImpulse(filter.Transform->Up);
            }

            if (input->MenuDown)
            {

            }

            if (input->Action)
            {
                filter.body3D->AddLinearImpulse(filter.Transform->Up);
                Debug.Log("action pressed");
            }

            if (input->Confirm)
            {

            }

            if (input->Back)
            {

            }
        }
    }

}
