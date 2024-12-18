using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public class HeldItem : MonoBehaviour
    {
        [field: SerializeField] public HeldItemType Type { get; protected set; }

        public bool IsBeingHeld { get; set; }
    }
}