using Fusion;
using UnityEngine;

namespace FusionDemo {
    /// <summary>
    /// Class responsible for make the player able to interact with Interactable objects on the world in host mode.
    /// </summary>
    public class PlayerInteractHost : NetworkBehaviour {
        [SerializeField] private float interactRadius = 1.25f;

        private NetworkButtons prevInputButtons;
        private Collider[] interactQueryResult = new Collider[5];

        public override void FixedUpdateNetwork() {
            if (GetInput<PlayerInputAction>(out var input)) {
                if (input.buttons.WasPressed(prevInputButtons, InputButton.INTERACT)) {
                    Vector3 radiusPos = transform.position + transform.forward * 1.5f;
                    var hits = Runner.GetPhysicsScene().OverlapSphere(radiusPos, interactRadius, interactQueryResult, 1, QueryTriggerInteraction.UseGlobal);
                    if (hits > 0) {
                        for (int i = 0; i < hits && i < interactQueryResult.Length; i++) {
                            if (interactQueryResult[i].TryGetComponent<IInteractable>(out var interactable)) {
                                interactable.Interact(Runner, Object.InputAuthority);
                                break;
                            }
                        }
                    }
                }

                prevInputButtons = input.buttons;
            }
        }

        private void OnDrawGizmosSelected() {
            // Draw the interact area when selected.
            Gizmos.DrawSphere(transform.position + transform.forward * 1.5f, interactRadius);
        }
    }
}