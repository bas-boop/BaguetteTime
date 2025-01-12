using UnityEngine;
using UnityEngine.InputSystem;

using Framework;
using Framework.Gameplay;

namespace Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rotator))]
    [RequireComponent(typeof(InteractManager))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Rotator rotator;
        [SerializeField] private InteractManager interactManager;
        [SerializeField] private SceneSwitcher sceneSwitcher;
        
        private PlayerInput _playerInput;
        private InputActionAsset _playerControlsActions;
        private bool _isWalking;
        private bool _canMove = true;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerControlsActions = _playerInput.actions;
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            
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
            if (!_canMove)
                return;
            
            Vector2 cameraInput = _playerControlsActions[InputActions.ROTATE_ACTION].ReadValue<Vector2>();
            rotator.Rotate(cameraInput);
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        public void CantMoveAnymore() => _canMove = false;

        private void AddListeners()
        {
            _playerControlsActions[InputActions.INTERACT_ACTION].performed += Interact;
            _playerControlsActions[InputActions.RESTART_ACTION].performed += Restart;
        }
        
        private void RemoveListeners()
        {
            _playerControlsActions[InputActions.INTERACT_ACTION].performed -= Interact;
            _playerControlsActions[InputActions.RESTART_ACTION].performed -= Restart;
        }

        private void Interact(InputAction.CallbackContext obj)
        {
            if (!_canMove)
                return;
            
            interactManager.CheckInteraction();
        }

        private void Restart(InputAction.CallbackContext obj)
        {
            if (sceneSwitcher)
                sceneSwitcher.LoadScene();
        }
    }
}