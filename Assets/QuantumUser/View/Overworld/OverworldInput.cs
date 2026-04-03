namespace Quantum
{
	using UnityEngine;
	using UnityEngine.InputSystem;
	using Photon.Deterministic;

	/// <summary>
	/// Handles player input.
	/// </summary>
	[DefaultExecutionOrder(-10)]
	public sealed class OverworldInput : MonoBehaviour
	{
		public static float LookSensitivity = 4.0f;

		/// <summary>
		/// This is a special accumulator which accepts mouse/touch delta and returns smoothed,
		/// frame-aligned look rotation delta. It is essential to get super snappy butter-smooth experience.
		/// </summary>
		public Vector2Accumulator LookRotationAccumulator => _lookRotationAccumulator;

		private Quantum.Input      _accumulatedInput;
		private Vector2Accumulator _lookRotationAccumulator = new(0.02f, true);
		private bool               _resetAccumulatedInput;
		private int                _lastAccumulateFrame;
		private bool               _jumpTouch;
		private float              _jumpTime;

		private void OnEnable() {
			QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
			AccumulateInput();
		}

		private void AccumulateInput()
		{
			if (_lastAccumulateFrame == Time.frameCount)
				return;

			_lastAccumulateFrame = Time.frameCount;

			if (_resetAccumulatedInput == true)
			{
				_resetAccumulatedInput = false;
				_accumulatedInput = default;
			}

			else
			{
				ProcessStandaloneInput();
			}
		}

		private void ProcessStandaloneInput()
		{
			// Enter key is used for locking/unlocking cursor in game view.
			Keyboard keyboard = Keyboard.current;
			if (keyboard != null && (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame))
			{
				if (Cursor.lockState == CursorLockMode.Locked)
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
				else
				{
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
			}

			// Accumulate input only if the cursor is locked.
			if (Cursor.lockState != CursorLockMode.Locked)
				return;

			Mouse mouse = Mouse.current;
			if (mouse != null)
			{
				Vector2 mouseDelta = mouse.delta.ReadValue();

				Vector2 lookRotationDelta = new Vector2(-mouseDelta.y, mouseDelta.x);
				lookRotationDelta *= LookSensitivity / 60f;
				_lookRotationAccumulator.Accumulate(lookRotationDelta);
			}

			if (keyboard != null)
			{
				Vector2 moveDirection = Vector2.zero;

				if (keyboard.wKey.isPressed) { moveDirection += Vector2.up;    }
				if (keyboard.sKey.isPressed) { moveDirection += Vector2.down;  }
				if (keyboard.aKey.isPressed) { moveDirection += Vector2.left;  }
				if (keyboard.dKey.isPressed) { moveDirection += Vector2.right; }

				_accumulatedInput.Direction = moveDirection.normalized.ToFPVector2();

				_accumulatedInput.Jump |= keyboard.spaceKey.isPressed;
			}
		}

		public void PollInput(CallbackPollInput callback)
		{
			AccumulateInput();

			_resetAccumulatedInput = true;

			Vector2   consumeLookRotation = _lookRotationAccumulator.ConsumeFrameAligned(callback.Game);
			FPVector2 pollLookRotation    = consumeLookRotation.ToFPVector2();

			_lookRotationAccumulator.Add(consumeLookRotation - pollLookRotation.ToUnityVector2());

			_accumulatedInput.LookRotationDelta = pollLookRotation;

			callback.SetInput(_accumulatedInput, DeterministicInputFlags.Repeatable);
		}
	}
}
