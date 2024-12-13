using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay
{
    public class BasicInteracter : Interactable
    {
        [SerializeField] private UnityEvent onInteraction = new();
        
        public override void DoInteraction() => onInteraction?.Invoke();
    }
}