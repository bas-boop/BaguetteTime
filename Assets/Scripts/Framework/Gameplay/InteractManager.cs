using UnityEngine;

namespace Framework.Gameplay
{
    public sealed class InteractManager : MonoBehaviour
    {
        [SerializeField] private Interactable[] interactables;

        public void CheckInteraction()
        {
            foreach (Interactable interactable in interactables)
            {
                if (!interactable.CanInteract)
                    continue;
                
                interactable.DoInteraction();
                break;
            }
        }
    }
}