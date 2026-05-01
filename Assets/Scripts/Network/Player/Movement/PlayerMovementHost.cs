using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo {
    /// <summary>
    /// A simple networked player movement class for host/server mode.
    /// </summary>
    [RequireComponent(typeof(NetworkCharacterController))]
    public class PlayerMovementHost : NetworkBehaviour
    {
        private NetworkCharacterController cc;
        [Networked] private NetworkButtons NetworkButtons { get; set; }

        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            cc = GetBehaviour<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork() {
            // If we received input from the input authority
            // The NetworkObject input authority AND the server/host will have the inputs
            if (GetInput<PlayerInputAction>(out var input))
            {
                if (BattleStateManager.singleton.state == BattleStateManager.BattleState.NOT_IN_BATTLE)
                {
                    // Overworld movement
                    cc.Move(input.moveDirection.normalized);
                }
                else
                {
                    if (BattleStateManager.singleton.state != BattleStateManager.BattleState.ENEMY_TURN)
                    {
                        // Player is allowed to move since it is their turn
                        cc.Move(input.moveDirection.normalized);
                    }
                    else
                    {
                        // Not player's turn so movement is disabled
                        return;
                    }
                }
                // Store the current buttons to use them on the next FUN (FixedUpdateNetwork) call
                NetworkButtons = input.buttons;
            }
        }
    }
}