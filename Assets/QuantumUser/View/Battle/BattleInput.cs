using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Battle
{
    public class BattleInput : MonoBehaviour
    {
        private void OnEnable()
        {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        public void PollInput(CallbackPollInput callback)
        {
            Quantum.Input i = new Quantum.Input();

            // Note: Use GetKey() instead of GetKeyDown/Up. Quantum calculates up/down internally.
            i.MenuLeft = UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.LeftArrow);
            i.MenuRight = UnityEngine.Input.GetKey(KeyCode.L) || UnityEngine.Input.GetKey(KeyCode.RightArrow);
            i.MenuUp = UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.UpArrow);
            i.MenuDown = UnityEngine.Input.GetKey(KeyCode.Semicolon) || UnityEngine.Input.GetKey(KeyCode.DownArrow);
            i.Action = UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.F) || UnityEngine.Input.GetKey(KeyCode.J) || UnityEngine.Input.GetKey(KeyCode.K);
            i.Confirm = UnityEngine.Input.GetKey(KeyCode.Space) || UnityEngine.Input.GetKey(KeyCode.LeftShift);
            i.Back = UnityEngine.Input.GetKey(KeyCode.Tab) || UnityEngine.Input.GetKey(KeyCode.DownArrow);

            callback.SetInput(i, DeterministicInputFlags.Repeatable);
        }
    }
}
