using Photon.Deterministic;
using UnityEngine;

namespace Quantum {
    public partial class RuntimePlayer {
        public GameObject spawnObject;
        [HideInInspector] public FPVector3 location;

        public FPVector3 GetSpawnPos() => ConvertTransformToFPV3(spawnObject.transform);

        private FPVector3 ConvertTransformToFPV3(Transform t) {
            return new((int) t.position.x, (int)t.position.y, (int)t.position.z);
        }
    }
}