namespace Quantum {
    using UnityEngine;
    using static Quantum.OverworldData;

    public class Spawnpoint : MonoBehaviour {
        public SpawnpointData Bake() {
            return new SpawnpointData {
                position = transform.position.ToFPVector3(),
                rotation = transform.rotation.ToFPQuaternion()
            };
        }
    }
}
