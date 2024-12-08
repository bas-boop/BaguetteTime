using UnityEngine;

using Framework.Attributes;

namespace Framework.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField, Tag] protected string p_playerTag = "Player"; 
        
        public bool CanInteract { get; private set; }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(p_playerTag)) 
                CanInteract = true;
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(p_playerTag))
                CanInteract = false;
        }

        public abstract void DoInteraction();
    }
}