using UnityEngine;

using Framework.Attributes;

namespace Player
{
    public sealed class Rotator : MonoBehaviour
    {
        [SerializeField] private float yawSensitivity = 1f;
        [SerializeField] private float pitchSensitivity = 1f;

        [SerializeField, RangeVector2(0, -180, 0, 180), Tooltip("X is for the minimal pitch. Y is for the maximal pitch")]
        private Vector2 minMaxPitch = new (-80, 80);

        private float _pitch;

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        public void Rotate(Vector2 input)
        {
            float yaw = input.x * yawSensitivity;
            _pitch -= input.y * pitchSensitivity;
            _pitch = Mathf.Clamp(_pitch, minMaxPitch.x, minMaxPitch.y);
            
            Quaternion targetRotation = Quaternion.Euler(_pitch, transform.eulerAngles.y + yaw, 0f);
            transform.rotation = targetRotation;
        }
    }
}