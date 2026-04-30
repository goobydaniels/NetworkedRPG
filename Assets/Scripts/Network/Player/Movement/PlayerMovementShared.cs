using Fusion;
using UnityEngine;
using UnityEngine.Windows;

namespace FusionDemo
{
    /// <summary>
    /// A simple networked player movement class for shared mode.
    /// </summary>
    [RequireComponent(typeof(NetworkCharacterController))]
    public class PlayerMovementShared : NetworkBehaviour
    {
        private NetworkCharacterController cc;

        public override void Spawned() {
            // get the NetworkCharacterController reference
            cc = GetBehaviour<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork() {
            var dir = GetMoveInput();

            if (BattleStateManager.singleton.state == BattleStateManager.BattleState.NOT_IN_BATTLE)
            {
                // Overworld movement
                cc.Move(dir.normalized);
            }
            else
            {
                // In battle movement, should move menus
                switch (BattleStateManager.singleton.state)
                {
                    case BattleStateManager.BattleState.PLAYER1_TURN:
                        // Do nothing because its not the clients turn and the enemy is not attacking
                        break;

                    case BattleStateManager.BattleState.PLAYER2_TURN:
                        // Should be for menu navigation
                        cc.Move(dir.normalized);
                        break;

                    case BattleStateManager.BattleState.ENEMY_TURN:
                        cc.Move(dir.normalized);
                        break;
                }
            }
        }

        private Vector3 GetMoveInput() {
            // initial direction, no movement
            var dir = Vector3.zero;

            // Handle horizontal input
            dir += Vector3.right * UnityEngine.Input.GetAxisRaw("Horizontal");

            // Handle vertical input
            dir += Vector3.forward * UnityEngine.Input.GetAxisRaw("Vertical");

            return dir.normalized;
        }
    }
}