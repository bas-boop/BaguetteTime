using Framework.Gameplay.HeldItemSystem;
using UnityEngine;

namespace Framework.Gameplay.Dough
{
    public sealed class Dough : HeldItem
    {
        [SerializeField] private Transform visual;
        [SerializeField] private Material bakedMat;

        public Transform GetVisual() => visual;

        public void Bread() => visual.GetComponent<MeshRenderer>().material = bakedMat;
    }
}