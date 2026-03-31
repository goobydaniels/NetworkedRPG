namespace Quantum
{
	using UnityEngine;
	using static OverworldData;

	public class GameplayDataBaker : MapDataBakerCallback {
		public override void OnBeforeBake(QuantumMapData data) {
		}

        // https://github.com/Abdullah165/quantum-kcc-sample/blob/main/Assets/Photon/QuantumAddons/KCC/Example/Scripts/View/Gameplay/GameplayDataBaker.cs#L20

        public override void OnBake(QuantumMapData data) {
#if UNITY_EDITOR
            OverworldData overworldData = QuantumUnityDB.GetGlobalAssetEditorInstance<OverworldData>((AssetRef)data.Asset.UserAsset.Id);

            overworldData.spawnPoints = new SpawnPointData[2];
			for (int i = 0; i < 2; ++i) {
                overworldData.spawnPoints[i] = spawnPoints[i].Bake();
			}

			Waypoint[] waypoints = GameObject.FindObjectsOfType<Waypoint>(true);
			gameplayData.Waypoints = new WaypointData[waypoints.Length];
			for (int i = 0; i < waypoints.Length; ++i)
			{
				gameplayData.Waypoints[i] = waypoints[i].Bake();
			}

			UnityEditor.EditorUtility.SetDirty(gameplayData);

			Debug.Log($"Baked {spawnPoints.Length} spawn points.");
			Debug.Log($"Baked {waypoints.Length} waypoints.");
#endif
		}
	}
}
