using Fusion;
using FusionDemo;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    public class PlayerJumpShared : NetworkBehaviour
    {
        // NetworkCharacterController
        private NetworkCharacterController _cc;
        private bool isInBattle;

        // Used to detect if the jump button was pressed in shared mode context.
        private bool _jumpPressed;

        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            _cc = GetBehaviour<NetworkCharacterController>();

            if (SceneManager.GetActiveScene().name == "BattleTesting")
            {
                isInBattle = true;
            }
            else
            {
                isInBattle = false;
            }
        }

        public override void FixedUpdateNetwork()
        {
            // If the Jump button was pressed.
            if (GetJumpInput())
            {
                _cc.Jump(false);
            }
            _jumpPressed = false;
        }

        #if !UNITY_IOS && !UNITY_ANDROID
        private void Update()
        {
            // Detect interact input in update and store it to use it in FUN.
            // if (Object.HasStateAuthority == false) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpPressed = true;
            }
        }
        #endif

        private bool GetJumpInput()
        {
            return _jumpPressed;
        }
    }

}