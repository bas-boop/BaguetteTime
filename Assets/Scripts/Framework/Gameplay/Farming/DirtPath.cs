using UnityEngine;

namespace Framework.Gameplay.Farming
{
    public sealed class DirtPath : Interactable
    {
        [SerializeField] private Grain grain;

        private void Start()
        {
            grain.gameObject.SetActive(false);
        }

        public override void DoInteraction()
        {
            grain.gameObject.SetActive(true);
        }
    }
}