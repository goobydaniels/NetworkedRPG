using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneTransitionTrigger : NetworkBehaviour {
    private NetworkBool IsSpawned { get; set; }
    private SceneTransitionTrigger trigger;

    [SerializeField] private string nextScene;
    [SerializeField] private List<GameObject> players = new();

    [Networked] private int playersInRadius { get; set; }

    public override void Spawned() {
        IsSpawned = true;
        trigger = this;
    }

    public override void FixedUpdateNetwork() {
        if (!Object.HasStateAuthority && !IsSpawned) return;

        if (playersInRadius >= Runner.ActivePlayers.Count()) {
            Runner.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (IsSpawned && other.CompareTag("Player")) {
            RPC_PlayerEnterStart();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (IsSpawned && other.CompareTag("Player")) {
            RPC_PlayerExitStart();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerEnterStart() {
        if (transform != null) {
            if (HasStateAuthority) {
                trigger.playersInRadius++;
            }
        }
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerExitStart() {
        if (IsSpawned) {
            if (trigger != null && HasStateAuthority) {
                trigger.playersInRadius--;
            }
        }
    }
}
