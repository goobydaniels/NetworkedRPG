using Fusion;
using Fusion.Menu;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace FusionDemo {
    public class DemoInputPooling : SimulationBehaviour, IBeforeUpdate, INetworkRunnerCallbacks {
        private PlayerInputAction inputSystem;
        private bool resetInput;

        void IBeforeUpdate.BeforeUpdate()
        {
            if (resetInput)
            {
                resetInput = false;
                inputSystem = default;
            }

            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && (keyboard.wasUpdatedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame || keyboard.escapeKey.wasPressedThisFrame))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            if (Cursor.lockState != CursorLockMode.Locked)
            {
                return;
            }

            NetworkButtons buttons = default;

            if (keyboard != null)
            {
                Vector2 moveDirection = Vector2.zero;

                if (keyboard.wKey.isPressed) moveDirection += Vector2.up;
                if (keyboard.sKey.isPressed) moveDirection += Vector2.down;
                if (keyboard.aKey.isPressed) moveDirection += Vector2.left;
                if (keyboard.dKey.isPressed) moveDirection += Vector2.right;

                inputSystem.moveDirection += moveDirection;

                buttons.Set(InputButton.INTERACT, keyboard.eKey.isPressed);
                buttons.Set(InputButton.JUMP, keyboard.spaceKey.isPressed);
            }

            inputSystem.buttons = new NetworkButtons(inputSystem.buttons.Bits | buttons.Bits);
        }
        
        // Pooling the input
        void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input)
        {
            inputSystem.moveDirection.Normalize();
            input.Set(inputSystem);
            resetInput = true;
        }

        #region Other callbacks

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (player == runner.LocalPlayer)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        async void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (shutdownReason == ShutdownReason.DisconnectedByPluginLogic)
            {
                await FindFirstObjectByType<FusionMenuConnectionBehaviourSdk>(FindObjectsInactive.Include).DisconnectAsync(ConnectFailReason.Disconnect);
                FindFirstObjectByType<FusionMenuUIGameplay>(FindObjectsInactive.Include).Controller.Show<FusionMenuUIMain>();
            }
        }

        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }

        #endregion
    }
}