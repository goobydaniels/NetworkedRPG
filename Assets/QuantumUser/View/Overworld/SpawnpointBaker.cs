using Quantum;

[assembly: QuantumMapBakeAssembly]
namespace Quantum.Editor {
    using UnityEditor;
    using UnityEngine;

    public class SpawnpointBaker : MapDataBakerCallback {
        public override void OnBake(QuantumMapData data) {
            var overworld = QuantumUnityDB.GetGlobalAssetEditorInstance<OverworldData>(data.GetAsset(true).UserAsset);
            Spawnpoint[] spawnpoints = Object.FindObjectsByType<Spawnpoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            if (!overworld || spawnpoints.Length == 0) return;

            if (overworld.defaultSpawnpoint.Equals(default(OverworldData.SpawnpointData))) {
                overworld.defaultSpawnpoint.position = spawnpoints[0].transform.position.ToFPVector3();
                overworld.defaultSpawnpoint.rotation = spawnpoints[0].transform.rotation.ToFPQuaternion();
            }

            overworld.spawnpoints = new OverworldData.SpawnpointData[spawnpoints.Length];
            for (int i = 0; i < spawnpoints.Length; i++) {
                overworld.spawnpoints[i].position = spawnpoints[i].transform.position.ToFPVector3();
                overworld.spawnpoints[i].rotation = spawnpoints[i].transform.rotation.ToFPQuaternion();
            }
            #if UNITY_EDITOR
            EditorUtility.SetDirty(overworld);
            #endif
        }

        public override void OnBeforeBake(QuantumMapData data) {
            throw new System.NotImplementedException();
        }
    }
}
