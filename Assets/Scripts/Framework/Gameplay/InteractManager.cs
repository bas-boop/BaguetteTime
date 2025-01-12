using UnityEngine;

namespace Framework.Gameplay
{
    public sealed class InteractManager : MonoBehaviour
    {
        [SerializeField] private Interactable[] interactables;
        [SerializeField] private GameObject buttonPrompt;

        public void CheckInteraction()
        {
            foreach (Interactable interactable in interactables)
            {
                Debug.Log(interactable.name);
                
                if (!interactable.CanInteract)
                    continue;
                
                Debug.Log(interactable.name + " succes");
                buttonPrompt.SetActive(false);
                interactable.DoInteraction();
                break;
            }
        }

        public void CheckDistance(Interactable target)
        {
            if (!target.CanInteract)
                return;
            
            buttonPrompt.SetActive(true);
        }
    }
}