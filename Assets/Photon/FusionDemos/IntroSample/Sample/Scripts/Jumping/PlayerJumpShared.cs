using Fusion;
using FusionDemo;
using UnityEngine;
using System;

namespace FusionDemo
{
    public class PlayerJumpShared : NetworkBehaviour
    {
        // NetworkCharacterController
        private NetworkCharacterController _cc;

        #if !UNITY_IOS && !UNITY_ANDROID
        // Used to detect if the interact button was pressed in shared mode context.
        private bool _jumpPressed;
        #endif

        #if UNITY_IOS || UNITY_ANDROID
        private MobileInput _mobileInput;

        private void Awake() {
          _mobileInput = FindFirstObjectByType<MobileInput>();
        }
        #endif

        public override void FixedUpdateNetwork()
        {
            // If the interact button was pressed.
            if (GetJumpInput())
            {
                _cc.Jump(false);
            }
            #if !UNITY_IOS && !UNITY_ANDROID
            _jumpPressed = false;
            #endif
        }

        #if !UNITY_IOS && !UNITY_ANDROID
        private void Update()
        {
            // Detect interact input in update and store it to use it in FUN.
            if (Object.HasStateAuthority == false) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpPressed = true;
            }
        }
        #endif

        private bool GetJumpInput()
        {
            #if UNITY_IOS || UNITY_ANDROID
                  return _mobileInput.ConsumeInteractInput();
            #else
            return _jumpPressed;
            #endif
        }
    }

}