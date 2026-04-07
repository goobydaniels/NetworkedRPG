namespace Quantum {

    using UnityEngine;

    public sealed class SceneContext : MonoBehaviour, IQuantumViewContext {
        public OverworldInput Input;

        public PlayerRef LocalPlayer;
        public EntityRef LocalPlayerEntity;
        public PlayerView LocalPlayerView;
    }
}
