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
                    if (BattleStateManager.singleton.state == BattleStateManager.BattleState.NOT_IN_BATTLE)
                    {
                        _cc.Jump(false);
                    }
                    else
                    {
                        switch (BattleStateManager.singleton.state)
                        {
                            case BattleStateManager.BattleState.PLAYER1_TURN:
                                // If in battle, if is turn, first space press is to select menu action, second space is
                                // to select target for attack
                                // then thrid is action command
                                // other wise is just jump
                                _cc.Jump(false);
                                break;

                            case BattleStateManager.BattleState.PLAYER2_TURN:
                                // Do nothing because its not the hosts turn and the enemy is not attacking
                                // Just here incase it's needed
                                break;

                            case BattleStateManager.BattleState.ENEMY_TURN:
                                _cc.Jump(false);
                                break;
                        }
                    }
                }

                // Store the input buttons to use on the next tick.
                _prevInputButtons = input.buttons;
            }
        }
    }
}
