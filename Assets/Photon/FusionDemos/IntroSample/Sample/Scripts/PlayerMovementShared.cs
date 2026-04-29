using Fusion;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo {
  /// <summary>
  /// A simple networked player movement class for shared mode.
  /// </summary>
  [RequireComponent(typeof(NetworkCharacterController))]
  public class PlayerMovementShared : NetworkBehaviour {
    private NetworkCharacterController _cc;
    private bool isInBattle = false;


#if UNITY_IOS || UNITY_ANDROID
    private MobileInput _mobileInput;

    private void Awake() {
      _mobileInput = FindFirstObjectByType<MobileInput>();
    }
#endif

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

        if (battleSystem != null && battleSystem.CurrentTurnPlayer != Runner.LocalPlayer)
        {
            // Not this players turn so no movement
            return;
        }

        var dir = GetMoveInput();

        // Move with the direction calculated
        _cc.Move(dir.normalized);

        Debug.Log("Playermovement");
    }

    private Vector3 GetMoveInput() {
      // initial direction, no movement
      var dir = Vector3.zero;

      // Handle horizontal input
      dir += Vector3.right * Input.GetAxisRaw("Horizontal");

      // Handle vertical input
      dir += Vector3.forward * Input.GetAxisRaw("Vertical");

      return dir.normalized;
    }
  }
}