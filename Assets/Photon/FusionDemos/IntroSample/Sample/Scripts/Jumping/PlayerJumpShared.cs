using Fusion;
using FusionDemo;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    public class PlayerJumpShared : NetworkBehaviour
    {
        private NetworkCharacterController _controller;

        private NetworkButtons _prevInputButtons;

        private void Awake()
        {
            _controller = GetBehaviour<NetworkCharacterController>();
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput<PlayerInputAction>(out var input))
            {
                // If the jump button was pressed.
                if (input.buttons.WasPressed(_prevInputButtons, InputButton.JUMP))
                {
                    _controller.Jump(false);
                }

                // Store the input buttons to use on the next tick.
                _prevInputButtons = input.buttons;
            }
        }
    }
}