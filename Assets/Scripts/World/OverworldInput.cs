namespace Quantum {
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Photon.Deterministic;

    /// <summary>
    /// Handles player input.
    /// </summary>
    [DefaultExecutionOrder(-10)]
    public sealed class OverworldInput : MonoBehaviour {
        public static float lookSensitivity = 4.0f;

        /// <summary>
        /// This is a special accumulator which accepts mouse/touch delta and returns smoothed,
        /// frame-aligned look rotation delta. It is essential to get super snappy butter-smooth experience.
        /// </summary>
        public Vector2Accumulator LookRotationAccumulator => lookRotAccum;

        private Input accumulatedInput;
        private Vector2Accumulator lookRotAccum = new(0.02f, true);
        private bool resetAccumInput;
        private int lastAccumFrame;
        private float jumpTime;

        private void OnEnable() {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        private void OnDisable() {
        }

        private void Update() {
            AccumulateInput();
        }

        private void AccumulateInput() {
            if (lastAccumFrame == Time.frameCount) return;

            lastAccumFrame = Time.frameCount;

            if (resetAccumInput == true) {
                resetAccumInput = false;
                accumulatedInput = default;
            } else {
                ProcessStandaloneInput();
            }
        }

        private void ProcessStandaloneInput() {
            // Enter key is used for locking/unlocking cursor in game view.
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)) {
                if (Cursor.lockState == CursorLockMode.Locked) {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                } else {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            // Accumulate input only if the cursor is locked.
            if (Cursor.lockState != CursorLockMode.Locked)
                return;

            Mouse mouse = Mouse.current;
            if (mouse != null) {
                Vector2 mouseDelta = mouse.delta.ReadValue();

                Vector2 lookRotationDelta = new(-mouseDelta.y, mouseDelta.x);
                lookRotationDelta *= lookSensitivity / 60f;
                lookRotAccum.Accumulate(lookRotationDelta);
            }

            if (keyboard != null) {
                Vector2 moveDirection = Vector2.zero;

                if (keyboard.wKey.isPressed) { moveDirection += Vector2.up; }
                if (keyboard.sKey.isPressed) { moveDirection += Vector2.down; }
                if (keyboard.aKey.isPressed) { moveDirection += Vector2.left; }
                if (keyboard.dKey.isPressed) { moveDirection += Vector2.right; }

                accumulatedInput.Direction = moveDirection.normalized.ToFPVector2();

                accumulatedInput.Jump |= keyboard.spaceKey.isPressed;
            }
        }

        public void PollInput(CallbackPollInput callback) {
            AccumulateInput();

            resetAccumInput = true;

            Vector2 consumeLookRotation = lookRotAccum.ConsumeFrameAligned(callback.Game);
            FPVector2 pollLookRotation = consumeLookRotation.ToFPVector2();

            lookRotAccum.Add(consumeLookRotation - pollLookRotation.ToUnityVector2());

            accumulatedInput.LookRotationDelta = pollLookRotation;

            callback.SetInput(accumulatedInput, DeterministicInputFlags.Repeatable);
        }
    }
}
