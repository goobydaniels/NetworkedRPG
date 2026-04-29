using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo {
  /// <summary>
  /// A simple networked player movement class for host/server mode.
  /// </summary>
  [RequireComponent(typeof(NetworkCharacterController))]
  public class PlayerMovementHost : NetworkBehaviour {
    private NetworkCharacterController _cc;
    [Networked] private bool isInBattle { get; set; }
    [Networked] private NetworkButtons NetworkButtons { get; set; }

    public override void Spawned() {
      // get the NetworkCharacterController reference
      _cc = GetBehaviour<NetworkCharacterController>();

        if (SceneManager.GetActiveScene().name == "BattleTesting")
        {
            isInBattle = true;
        }
    }

    public override void FixedUpdateNetwork() {
        var battleSystem = FindFirstObjectByType<BattleSystemHost>();

        if (battleSystem != null && battleSystem.CurrentTurnPlayer != Object.InputAuthority)
        {
            // If its NOT this player's current time
            return;
        }

        // If we received input from the input authority
        // The NetworkObject input authority AND the server/host will have the inputs
        if (GetInput<PlayerInputAction>(out var input))
        {
            if (isInBattle)
            {
                // Movement is handled by PlayerBattleMovementHost after this point
                return;
            }

            _cc.Move(input.moveDirection.normalized);

            // Store the current buttons to use them on the next FUN (FixedUpdateNetwork) call
            NetworkButtons = input.buttons;
      }
    }
  }
}