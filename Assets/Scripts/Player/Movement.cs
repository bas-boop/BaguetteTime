#pragma warning disable CS0618 // Type or member is obsolete

using UnityEngine;

using Framework.Extensions;
using Framework.Gameplay;
using Player.Input;

namespace Player
{
    [RequireComponent(typeof(UniversalGroundChecker), 
        typeof(Rigidbody),
        typeof(InputParser))]
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        
        [SerializeField, Range(0.1f, 50)] private float moveSpeed = 3;
        [SerializeField] private float deceleration = 2;

        private UniversalGroundChecker _groundChecker;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _groundChecker = GetComponent<UniversalGroundChecker>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Walk(Vector2 moveInput)
        {
            if (!_groundChecker.IsGrounded)
                return;
            
            Vector3 newVelocity = CalculateDirection(moveInput) * moveSpeed;
            _rigidbody.velocity = new (newVelocity.x, _rigidbody.velocity.y, newVelocity.z);
        }
        
        public void SetIdle()
        {
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.Divide(deceleration, 1, deceleration);
            _rigidbody.velocity = newVelocity;
        }

        private Vector3 CalculateDirection(Vector2 moveInput)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            Vector3 direction = cameraForward * moveInput.y + cameraRight * moveInput.x;
            
            direction.y = 0;
            direction.Normalize();
            
            return direction;
        }
    }
}