using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.HeldItemSystem
{
    public abstract class ItemTaker : MonoBehaviour
    {
        [SerializeField] private ItemHolder itemHolder;
        [SerializeField] protected UnityEvent onTakeItem = new();

        protected HeldItem p_takenItem;
        
        public void TakeItem(HeldItem targetItem)
        {
            HeldItem existingItem = itemHolder.GetItemFormHolder(targetItem);
            
            if (existingItem)
                SuccessfulTake(existingItem);
        }

        public void TakeItem(int targetItem) => TakeItem((HeldItemType) targetItem);
        
        public void TakeItem(HeldItemType targetItem)
        {
            HeldItem existingItem = itemHolder.GetItemFormHolder(targetItem);

            if (existingItem)
                SuccessfulTake(existingItem);
        }

        public void GiveBackItem()
        {
            if (p_takenItem)
                itemHolder.HoldItem(p_takenItem);
        }

        private void SuccessfulTake(HeldItem existingItem)
        {
            p_takenItem = existingItem;
            p_takenItem.IsBeingHeld = false;
            TakeAction();
            onTakeItem?.Invoke();
        }

        public abstract void TakeAction();
    }
}