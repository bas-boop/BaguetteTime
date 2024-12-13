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
            
            if(!existingItem)
                return;

            if (!itemHolder.ReleaseItem(targetItem))
                throw new ($"Something broke here in {nameof(gameObject)}.");
            
            p_takenItem = existingItem;
            TakeAction();
            onTakeItem?.Invoke();
        }

        public abstract void TakeAction();
    }
}