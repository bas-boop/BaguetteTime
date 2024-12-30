using Framework.Gameplay.HeldItemSystem;
using UnityEngine;

namespace Framework.Gameplay.Dough
{
    public sealed class Dough : HeldItem
    {
        [SerializeField] private Transform visual;

        public Transform GetVisual() => visual;
    }
}