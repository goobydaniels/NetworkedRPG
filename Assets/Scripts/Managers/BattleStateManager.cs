using Fusion;
using UnityEngine;

public class BattleStateManager : NetworkBehaviour
{
    public enum BattleState
    {
        NOT_IN_BATTLE,
        IN_BATTLE,
        PLAYER1_TURN,
        PLAYER2_TURN,
        ENEMY_TURN,
        END
    }

    public static BattleStateManager BattleInstance
    {
        get => singleton;
        set
        {
            if (value == null)
            {
                singleton = null;
            }
            else if (singleton == null)
            {
                singleton = value;
            }
            else if (singleton != value)
            {
                Destroy(value);
                Debug.LogError($"There should only ever be one instance of {nameof(BattleStateManager)}!");
            }
        }
    }

    public static BattleStateManager singleton;
    [Networked] public BattleState state { get; set; }
    public BattleState GetBattleState => Object != null && Object.IsValid ? state : BattleState.NOT_IN_BATTLE;

    public void SetBattleState(BattleState nState)
    {
        if (Object == null || !Object.IsValid)
        {
            Debug.LogWarning("Tried to set BattleState before spawn");
            return;
        }

        if (!Object.HasStateAuthority)
        {
            Debug.LogWarning("Tried to set BattleState without StateAuthority");
            return;
        }

        state = nState;
    }

    public override void Spawned()
    {
        BattleInstance = this;

        if (Object.HasStateAuthority)
        {
            state = BattleState.NOT_IN_BATTLE;
        }
    }
}
