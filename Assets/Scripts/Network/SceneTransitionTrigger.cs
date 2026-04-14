using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static GameStateHandler;

// https://bitbucket.champlain.edu/projects/S25E321021/repos/sp25-egd320-s101-t02-p02/browse/Water%20Dept/Assets/Scripts/Events/EndZone.cs

public class SceneTransitionTrigger : NetworkBehaviour {
    [SerializeField] private string nextScene;

    [Networked] private TickTimer Timer { get; set; }
    [SerializeField] private float timeToComplete;

    private bool canStart;
    private int playersInRadius = 0;
    private States currentState = States.TRANSITION_TRIGGER;

    public UnityEvent TriggerTransition;

    public override void Spawned() {
        canStart = true;
    }

    private void FixedUpdate() {
        if (canStart && Instance.GetGameState == GameState.STARTED) {
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
            playersInRadius++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (currentState == States.TRANSITION_TRIGGER && other.CompareTag("Player")) {
            playersInRadius--;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    private void RPC_TriggerTransitionSignal() => RPC_TriggerTransition();

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_TriggerTransition() => TriggerSceneTransition();

    private void TriggerSceneTransition() {
        TriggerTransition?.Invoke();
        Instance.SetGameState(GameState.STARTED);
    }
}
