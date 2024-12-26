using UnityEngine;

using Framework.Attributes;

namespace Framework.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField, Tag] protected string p_playerTag = "Player";

        protected GameObject p_player;
        protected InteractionState p_currentState;
        
        public bool CanInteract { get; private set; }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(p_playerTag))
                return;
            
            CanInteract = true;
            p_player = other.gameObject;
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(p_playerTag))
                return;
            
            CanInteract = false;
            p_player = null;
        }

        public abstract void DoInteraction();
        
        
        public void SetStateToDone() => p_currentState = InteractionState.DONE;
    }
}