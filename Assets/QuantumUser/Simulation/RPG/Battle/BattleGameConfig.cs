using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Battle
{
    public class BattleGameConfig: AssetObject
    {
        [Header("Battle configuration")]
        [Tooltip("Enemy prototype ref")]
        public AssetRef<EntityPrototype> enemyPrototype;
        [Tooltip("Player 1 prototype ref")]
        public AssetRef<EntityPrototype> playerPrototype;
        [Tooltip("Player 2 prototype ref")]
        public AssetRef<EntityPrototype> playerPrototype2;
        [Tooltip("Player 3 prototype ref")]
        public AssetRef<EntityPrototype> playerPrototype3;
        [Tooltip("Player 4 prototype ref")]
        public AssetRef<EntityPrototype> playerPrototype4;
        [Tooltip("Starting number of enemies in ecnounter")]
        public int startingNumOfEnemies;
        [Tooltip("Number of waves in ecnounter")]
        public int numOfWaves;
    }
}
