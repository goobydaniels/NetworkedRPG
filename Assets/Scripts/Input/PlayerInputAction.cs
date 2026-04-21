using Fusion;
using UnityEngine;

public enum InputButton {
    INTERACT,
    JUMP
}

public struct PlayerInputAction : INetworkInput {
    public Vector3 moveDirection;
    public Vector2 lookDelta;
    public Vector2 cameraDirection;

    public NetworkButtons buttons;

    public PlayerInputAction(PlayerInputAction action) {
        moveDirection = action.moveDirection;
        lookDelta = action.lookDelta;
        cameraDirection = action.cameraDirection;
        buttons = action.buttons;
    }
}
