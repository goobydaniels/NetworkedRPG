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
        private bool isInBattle;

        // Previous NetworkButtons. Used to detect if a button was pressed in the previous tick.
        private NetworkButtons _prevInputButtons;

        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            _cc = GetBehaviour<NetworkCharacterController>();

            if (SceneManager.GetActiveScene().name == "BattleTesting")
            {
                isInBattle = true;
            }
        }

        public override void FixedUpdateNetwork()
        {
            // Get input for this tick.
            if (Object.HasInputAuthority && GetInput<PlayerInputAction>(out var input))
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

        private void OnDrawGizmosSelected()
        {
            // Draw the interact area when selected.
            // Gizmos.DrawSphere(transform.position + transform.forward * 1.5f, _interactRadius);
        }
    }
}
