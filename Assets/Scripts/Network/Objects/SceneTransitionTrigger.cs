using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameStateHandler;

public class SceneTransitionTrigger : NetworkBehaviour {
    [SerializeField] private string nextScene;

    [Networked] private TickTimer Timer { get; set; }
    [SerializeField] private float timeToComplete;

    private bool canStart;
    private int playersInRadius = 0;
    private List<GameObject> players = new();
    private States currentState = States.TRANSITION_TRIGGER;

    public override void Spawned() {
        canStart = true;
    }

    private void FixedUpdate() {
        if (canStart) {
            if (currentState == States.TRANSITION_TRIGGER && playersInRadius == Runner.ActivePlayers.Count()) {
                if (!Timer.IsRunning) {
                    Timer = TickTimer.CreateFromSeconds(Runner, timeToComplete);
                } else if (Timer.ExpiredOrNotRunning(Runner)) {
                    Timer = TickTimer.None;
                    RPC_TriggerTransitionSignal();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (currentState == States.TRANSITION_TRIGGER && other.CompareTag("Player")) {
            RPC_PlayerEnter(other.GetComponent<NetworkCharacterController>().Object.Id);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (currentState == States.TRANSITION_TRIGGER && other.CompareTag("Player")) {
            RPC_PlayerExit(other.GetComponent<NetworkCharacterController>().Object.Id);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerEnter(NetworkId playerID) {
        GameObject playerRef = Runner.FindObject(playerID).gameObject;

        if (playerRef) {
            if (!players.Contains(playerRef) && HasStateAuthority) {
                playersInRadius++;
                players.Add(playerRef);
            }

        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayerExit(NetworkId playerID) {
        GameObject playerRef = Runner.FindObject(playerID).gameObject;
        if (playerRef && HasStateAuthority) {
            if (players.Contains(playerRef)) {
                players.Remove(playerRef);
                playersInRadius--;
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    private void RPC_TriggerTransitionSignal() => RPC_TriggerTransition();

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_TriggerTransition() => TriggerSceneTransition();

    private void TriggerSceneTransition() {
        Runner.LoadScene(nextScene);
    }
}
