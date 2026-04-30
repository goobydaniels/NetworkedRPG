using Fusion;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FusionDemo
{
    public class BattleSystemHost : NetworkBehaviour, IAfterSpawned
    {
        [Networked, OnChangedRender(nameof(OnTurnChanged))]
        public PlayerRef CurrentTurnPlayer { get; set; }

        // Use [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)] to send actions from client to server/host
        // [Rpc(RpcSources.StateAuthority, RpcTargets.All)]

        void IAfterSpawned.AfterSpawned()
        {
            // Now safe — everything is spawned
            CurrentTurnPlayer = Runner.LocalPlayer;
        }

        public void OnBattleStart()
        {
            Debug.Log("BattleStart");

            if (!Object.HasStateAuthority)
            {
                return;
            }

            GameStateHandler.Instance.SetGameState(GameStateHandler.GameState.IN_BATTLE);

            BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.IN_BATTLE);

            CurrentTurnPlayer = Runner.LocalPlayer;
        }

        // Called when the turn should be advanced to the next in line and updates the current turn player
        public void NextTurn(PlayerRef playerRefForNextTurn)
        {
            // Checks for State Authority
            if (Object.HasStateAuthority)
            {
                switch (BattleStateManager.singleton.state)
                {
                    case BattleStateManager.BattleState.PLAYER1_TURN:
                        BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.PLAYER2_TURN);
                        break;

                    case BattleStateManager.BattleState.PLAYER2_TURN:
                        BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.ENEMY_TURN);
                        break;

                    case BattleStateManager.BattleState.ENEMY_TURN:
                        BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.PLAYER1_TURN);
                        break;
                }

                CurrentTurnPlayer = playerRefForNextTurn;
            }
        }

        // Is called whenever CurrentTurnPlayer is updated, just here to make sure the first turn is always the hosts
        void OnTurnChanged()
        {
            Debug.Log($"Turn changed: {CurrentTurnPlayer}");

            if (Runner.LocalPlayer == CurrentTurnPlayer)
            {
                BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.PLAYER1_TURN);
                Debug.Log("It's MY turn");
            }
            else
            {
                Debug.Log("It's someone else's turn");
            }
        }

        void EndBattle()
        {
            BattleStateManager.singleton.SetBattleState(BattleStateManager.BattleState.END);
        }
    }
}
