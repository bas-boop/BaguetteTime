using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public class ItemGiver : Interactable
    {
        [SerializeField] private HeldItem itemToGive;

        private ItemHolder _cached;
        
        public override void DoInteraction()
        {
            if(_cached == null)
                _cached = p_player.GetComponent<ItemHolder>();
            
            _cached.CreateAndHoldItem(itemToGive);
        }
    }
}