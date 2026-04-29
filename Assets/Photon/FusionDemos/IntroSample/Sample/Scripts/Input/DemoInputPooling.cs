using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FusionDemo {
    public class DemoInputPooling : MonoBehaviour, INetworkRunnerCallbacks {
        // Pooling the input
        public void OnInput(NetworkRunner runner, NetworkInput input) {
            PlayerInputAction inputSystem = new();
            Keyboard keyboard = Keyboard.current;

            Vector3 moveDirection = Vector3.zero;

            // Move around world
            if (keyboard.wKey.isPressed) moveDirection += Vector3.forward;
            if (keyboard.sKey.isPressed) moveDirection += Vector3.back;
            if (keyboard.aKey.isPressed) moveDirection += Vector3.left;
            if (keyboard.dKey.isPressed) moveDirection += Vector3.right;
            

            inputSystem.moveDirection = moveDirection.normalized;

            inputSystem.buttons.Set(InputButton.INTERACT, keyboard.eKey.isPressed);
            inputSystem.buttons.Set(InputButton.JUMP, keyboard.spaceKey.isPressed);

            input.Set(inputSystem);
        }

        #region Other callbacks

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
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
        public void OnSceneLoadDone(NetworkRunner runner)
        {
            // Find the battle system in the newly loaded scene
            var battleSystem = FindFirstObjectByType<BattleSystemHost>();
            Debug.Log("OnSceneLoadDone");

            if (battleSystem != null)
            {
                battleSystem.OnBattleStart();
            }
        }
        public void OnSceneLoadStart(NetworkRunner runner) { }

        #endregion
    }
}