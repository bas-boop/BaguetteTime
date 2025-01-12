using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;

namespace Framework.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField, Tag] protected string p_playerTag = "Player";
        [SerializeField] private UnityEvent onEnter = new();
        [SerializeField] private UnityEvent onExit = new();

        protected GameObject p_player;
        protected Collider p_collider;
        protected InteractionState p_currentState;
        
        public bool CanInteract { get; protected set; }

        private void Awake() => p_collider = GetComponent<Collider>();

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(p_playerTag))
                return;
            
            CanInteract = true;
            p_player = other.gameObject;
            onEnter?.Invoke();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(p_playerTag))
                return;
            
            CanInteract = false;
            p_player = null;
            onExit?.Invoke();
        }

        public abstract void DoInteraction();
        
        
        public virtual void SetStateToDone() => p_currentState = InteractionState.DONE;
    }
}