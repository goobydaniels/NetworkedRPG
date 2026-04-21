using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    /// <summary>
    /// Class responsible for make the player able to interact with Interactable objects on the world in host mode.
    /// </summary>
    public class PlayerJumpHost : NetworkBehaviour
    {
        private NetworkCharacterController _cc;

        // Previous NetworkButtons. Used to detect if a button was pressed in the previous tick.
        private NetworkButtons _prevInputButtons;

        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            _cc = GetBehaviour<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork()
        {
            // Get input for this tick.
            if (GetInput<PlayerInputAction>(out var input))
            {
                // If the jump button was pressed.
                if (input.buttons.WasPressed(_prevInputButtons, InputButton.JUMP))
                {
                    _cc.Jump(false);
                }

                // Store the input buttons to use on the next tick.
                _prevInputButtons = input.buttons;
            }
        }
    }
}
