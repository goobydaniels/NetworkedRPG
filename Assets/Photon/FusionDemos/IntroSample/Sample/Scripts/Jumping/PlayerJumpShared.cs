using Fusion;
using FusionDemo;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    public class PlayerJumpShared : NetworkBehaviour
    {
        private NetworkCharacterController _cc;

        private NetworkButtons _prevInputButtons;

        private void Awake()
        {
            _cc = GetBehaviour<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput<PlayerInputAction>(out var input))
            {
                // If the jump button was pressed.
                if (input.buttons.WasPressed(_prevInputButtons, InputButton.JUMP))
                {
                    if (BattleStateManager.singleton.state == BattleStateManager.BattleState.IN_BATTLE)
                    {
                        _cc.Jump(false);
                    }
                    else
                    {
                        switch (BattleStateManager.singleton.state)
                        {
                            case BattleStateManager.BattleState.PLAYER1_TURN:
                                // Do nothing because its not the clients turn and the enemy is not attacking
                                break;

                            case BattleStateManager.BattleState.PLAYER2_TURN:
                                // If in battle, if is turn, first space press is to select menu action, second space is
                                // to select target for attack
                                // then thrid is action command
                                // other wise is just jump
                                _cc.Jump(false);
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