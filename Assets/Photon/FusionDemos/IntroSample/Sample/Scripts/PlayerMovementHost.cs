using Fusion;
using UnityEngine;

namespace FusionDemo {
  /// <summary>
  /// A simple networked player movement class for host/server mode.
  /// </summary>
  [RequireComponent(typeof(NetworkCharacterController))]
  public class PlayerMovementHost : NetworkBehaviour {
    private NetworkCharacterController _cc;
    
    [Networked] private NetworkButtons NetworkButtons { get; set; }

    public override void Spawned() {
      // get the NetworkCharacterController reference
      _cc = GetBehaviour<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork() {
      // If we received input from the input authority
      // The NetworkObject input authority AND the server/host will have the inputs
      if (GetInput<PlayerInputAction>(out var input)) {

        _cc.Move(input.moveDirection.normalized);

        // Store the current buttons to use them on the next FUN (FixedUpdateNetwork) call
        NetworkButtons = input.buttons;
      }
    }
  }
}