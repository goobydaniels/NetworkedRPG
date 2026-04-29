using Fusion;
using UnityEngine;

namespace FusionDemo
{
    public class BattleSystemHost : NetworkBehaviour
    {
        [Networked, OnChangedRender(nameof(OnTurnChanged))]
        public PlayerRef CurrentTurnPlayer { get; set; }
        public bool playerOrEnemyTurn { get; private set; }

        // Use [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)] to send actions from client to server/host
        // [Rpc(RpcSources.StateAuthority, RpcTargets.All)]

        public void OnBattleStart()
        {
            Debug.Log("BattleStart");

            if (!Object.HasStateAuthority)
            {
                return;
            }

            GameStateHandler.Instance.SetGameState(GameStateHandler.GameState.IN_BATTLE);

            CurrentTurnPlayer = Runner.LocalPlayer;
        }

        public void NextTurn()
        {
            if (Object.HasStateAuthority)
            {
                // Logic to select next player
                // Only works for two players rn
                foreach (var player in Runner.ActivePlayers)
                {
                    if (player == Runner.LocalPlayer && !Runner.IsSharedModeMasterClient)
                    {
                        // This player is the Master Client (Host equivalent)
                        CurrentTurnPlayer = player;
                    }
                }
            }
        }

        void OnTurnChanged()
        {
            Debug.Log($"Turn changed: {CurrentTurnPlayer}");

            if (Runner.LocalPlayer == CurrentTurnPlayer)
            {
                Debug.Log("It's MY turn");
            }
            else
            {
                Debug.Log("It's someone else's turn");
            }
        }
    }
}


