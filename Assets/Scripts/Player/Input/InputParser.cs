using UnityEngine;
using UnityEngine.InputSystem;

using Framework.DebugSystem;

namespace Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Rotator rotator;
        [SerializeField] private Messenger test;
        
        private PlayerInput _playerInput;
        private InputActionAsset _playerControlsActions;
        private bool _isWalking;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerControlsActions = _playerInput.actions;
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _playerControlsActions[InputActions.MOVE_ACTION].ReadValue<Vector2>();

            if (moveInput == Vector2.zero)
            {
                if (!_isWalking) 
                    return;
                
                movement.SetIdle();
                _isWalking = false;
            }
            else
            {
                movement.Walk(moveInput);
                _isWalking = true;
            }
        }

        private void LateUpdate()
        {
            Vector2 cameraInput = _playerControlsActions[InputActions.ROTATE_ACTION].ReadValue<Vector2>();
            rotator.Rotate(cameraInput);
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            _playerControlsActions[InputActions.INTERACT_ACTION].performed += Interact;
        }
        
        private void RemoveListeners()
        {
            _playerControlsActions[InputActions.INTERACT_ACTION].performed += Interact;
        }

        private void Interact(InputAction.CallbackContext obj) => test.DebugLog("Interact");
    }
}