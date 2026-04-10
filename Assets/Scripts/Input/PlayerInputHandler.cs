using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : SimulationBehaviour, IBeforeUpdate, INetworkRunnerCallbacks {
    private InputSystem_Actions.OverWorldActions inputSystem;
    private Vector2 moveDirection, lookDelta;
    private float interactPress;
    private bool resetInput;

    public PlayerInputAction accumulatedInput = new();
    public Action<PlayerInputAction> OnInputSignal;

    private void Awake() {
        inputSystem = new InputSystem_Actions.OverWorldActions();

        inputSystem.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        inputSystem.Move.canceled += ctx => moveDirection = ctx.ReadValue<Vector2>();
        inputSystem.Look.performed += ctx => lookDelta = ctx.ReadValue<Vector2>();
        inputSystem.Look.canceled += ctx => lookDelta = ctx.ReadValue<Vector2>();

        inputSystem.Interact.started += ctx => interactPress = ctx.ReadValue<float>();
    }

    private void Start() {
        OnInputSignal?.Invoke(new PlayerInputAction(accumulatedInput));
    }

    private void OnEnable() {
        inputSystem.Enable();
    }

    private void OnDisable() {
        inputSystem.Disable();
    }

    public void BeforeUpdate() {
        if (resetInput) {
            resetInput = false;
            accumulatedInput = default;
        }

        NetworkButtons buttons = default;

        bool interactDown = (inputSystem.Interact.ReadValue<float>() > 0.0f);
        buttons.Set(InputButton.INTERACT, interactDown);

        accumulatedInput.moveDirection += moveDirection;
        accumulatedInput.lookDelta = lookDelta;

        Vector3 camDir = Camera.main.transform.forward;
        accumulatedInput.cameraDirection = new Vector2(camDir.x, camDir.z);

        accumulatedInput.buttons = new NetworkButtons(accumulatedInput.buttons.Bits | buttons.Bits);
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        if (accumulatedInput.moveDirection.magnitude > 1) {
            accumulatedInput.moveDirection.Normalize();
        }
        bool isValidInput = input.Set(accumulatedInput);
        resetInput = isValidInput;
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner) {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner) {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner) {
        throw new NotImplementedException();
    }
}
