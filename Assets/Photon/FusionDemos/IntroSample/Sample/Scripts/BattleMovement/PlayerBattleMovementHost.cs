using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    [RequireComponent(typeof(NetworkCharacterController))]
    public class PlayerBattleMovementHost : NetworkBehaviour
    {
        private NetworkCharacterController _cc;
        private bool isNotInBattle = true;

        [Networked] private NetworkButtons NetworkButtons { get; set; }


        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            _cc = GetBehaviour<NetworkCharacterController>();

            if (SceneManager.GetActiveScene().name != "BattleTesting")
            {
                isNotInBattle = true;
            }
        }

        public override void FixedUpdateNetwork()
        {
            // If we received input from the input authority
            // The NetworkObject input authority AND the server/host will have the inputs
            if (Object.HasInputAuthority && GetInput<PlayerInputAction>(out var input))
            {
                if (isNotInBattle)
                {
                    // Movement is handled by PlayerBattleMovementHost after this point
                    return;
                }

                _cc.Move(input.moveDirection.normalized);

                // Store the current buttons to use them on the next FUN (FixedUpdateNetwork) call
                NetworkButtons = input.buttons;
            }
        }
    }
}
