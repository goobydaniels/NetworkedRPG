using Fusion;
using UnityEngine;

public class GameStateHandler : NetworkBehaviour
{
    public enum GameState {
        NOT_STARTED,
        STARTED,
        IN_BATTLE,
        END
    }

    public static GameStateHandler Instance {
        get => singleton;
        set {
            if (value == null) {
                singleton = null;
            } else if (singleton == null) {
                singleton = value;
            } else if (singleton != value) {
                Destroy(value);
                Debug.LogError($"There should only ever be one instance of {nameof(GameStateHandler)}!");
            }
        }
    }

    public static GameStateHandler singleton;
    [Networked] public GameState state { get; set; }

    public GameState GetGameState => Object != null && Object.IsValid ? state : GameState.NOT_STARTED;
    public void SetGameState(GameState nState)
    {
        if (Object == null || !Object.IsValid)
        {
            Debug.LogWarning("Tried to set GameState before spawn");
            return;
        }

        if (!Object.HasStateAuthority)
        {
            Debug.LogWarning("Tried to set GameState without StateAuthority");
            return;
        }

        state = nState;
    }

    public override void Spawned()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Object.HasStateAuthority)
        {
            state = GameState.NOT_STARTED;
        }
    }
}