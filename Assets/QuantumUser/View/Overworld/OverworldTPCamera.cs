namespace Quantum {
    using UnityEngine;

    public class ThirdPersonCamera : QuantumEntityViewComponent<SceneContext> {
        public Transform pivot;
        public Transform handle;

        private float cameraDistance;

        public void Awake() {
            cameraDistance = handle.localPosition.z;
        }

        public override void OnLateUpdateView() {
            KCC kcc = GetPredictedQuantumComponent<KCC>();

            float lookPitch = kcc.Data.LookPitch.AsFloat;
            float lookYaw = kcc.Data.LookYaw.AsFloat;

            if (entityRef == ViewContext.LocalPlayerEntity) {
                lookPitch += ViewContext.Input.LookRotationAccumulator.AccumulatedValue.x;
                lookYaw += ViewContext.Input.LookRotationAccumulator.AccumulatedValue.y;

                lookPitch = Mathf.Clamp(lookPitch, -90.0f, 90.0f);

                while (lookYaw > 180.0f) { lookYaw -= 360.0f; }
                while (lookYaw < -180.0f) { lookYaw += 360.0f; }

                // For local player we also set transform rotation => this overrides default interpolation.
                transform.rotation = Quaternion.Euler(0.0f, lookYaw, 0.0f);
            }

            // Pivot and Handle transforms are updated for every player.
            pivot.localRotation = Quaternion.Euler(lookPitch, 0.0f, 0.0f);
            handle.localPosition = new Vector3(0.0f, 0.0f, cameraDistance);

            if (entityRef == ViewContext.LocalPlayerEntity) {
                // Only local player propagates Handle transform to main Camera.
                handle.GetPositionAndRotation(out Vector3 cameraPosition, out Quaternion cameraRotation);
                Camera.main.transform.SetPositionAndRotation(cameraPosition, cameraRotation);
            }
        }
    }
}
