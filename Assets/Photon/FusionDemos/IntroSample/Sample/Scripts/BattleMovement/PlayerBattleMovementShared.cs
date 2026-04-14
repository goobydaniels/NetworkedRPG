using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FusionDemo
{
    [RequireComponent(typeof(NetworkCharacterController))]
    public class PlayerBattleMovementShared : NetworkBehaviour
    {
        private NetworkCharacterController _cc;
        private bool isNotInBattle = true;

        public override void Spawned()
        {
            // get the NetworkCharacterController reference
            _cc = GetBehaviour<NetworkCharacterController>();

            if (SceneManager.GetActiveScene().name != "BattleTesting")
            {
                isNotInBattle = true;
            }
        }

        // This essenitally uses horizontal and vertical inputs for menu navigation and waits for jump as the confirm for an action
        public override void FixedUpdateNetwork()
        {
            // Checks if player is in battle or not
            if (isNotInBattle)
            {
                // Movement is handled by PlayerMovementShared after this point
                return;
            }

            var dir = GetMoveInput();

            // Move with the direction calculated
            _cc.Move(dir.normalized);

            Debug.Log("battleMovement");
        }

        private Vector3 GetMoveInput()
        {
            // initial direction, no movement
            var dir = Vector3.zero;

            // Handle horizontal input
            // dir += Vector3.right * Input.GetAxisRaw("Horizontal");

            // Handle vertical input
            // dir += Vector3.forward * Input.GetAxisRaw("Vertical");

            return dir.normalized;
        }
    }
}
