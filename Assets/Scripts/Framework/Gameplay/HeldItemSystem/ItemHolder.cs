using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public sealed class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Paranter paranter;
        [SerializeField] private List<HeldItem> heldItems;
        [SerializeField, Tooltip("There could only be 1 held item shown.")] private HeldItem visibleItem;

        public void HoldItem(HeldItem targetItem)
        {
            if (targetItem.IsBeingHeld)
                return;

            AddItem(targetItem);
        }
        
        public bool ReleaseItem(HeldItem targetItem)
        {
            if (!targetItem.IsBeingHeld
                || !heldItems.Contains(targetItem))
                return false;
            
            heldItems.Remove(targetItem);
            targetItem.IsBeingHeld = false;
            visibleItem = null;
            UpdateItemToShow();
            return true;
        }
        
        public bool ReleaseItem(int targetIndex)
        {
            if (heldItems.Count == 0
                || heldItems.Count < targetIndex)
                return false;
            
            heldItems[targetIndex].IsBeingHeld = false;
            heldItems.RemoveAt(targetIndex);
            visibleItem = null;
            UpdateItemToShow();
            return true;
        }
        
        public HeldItem CreateAndHoldItem(HeldItem targetItem)
        {
            HeldItem newItem = Instantiate(targetItem);
            AddItem(newItem);
            return newItem;
        }

        public HeldItem GetItemFormHolder(HeldItem targetItem)
        {
            if (!heldItems.Contains(targetItem))
                return null;
            
            heldItems.Remove(targetItem);
            UpdateItemToShow();
            targetItem.gameObject.SetActive(true);
            return targetItem;

        }

        public HeldItem GetItemFormHolder(HeldItemType targetItem)
        {
            foreach (HeldItem item in heldItems.Where(item => item.Type == targetItem))
            {
                heldItems.Remove(item);
                UpdateItemToShow();
                item.gameObject.SetActive(true);
                return item;
            }

            return null;
        }
        
        private void AddItem(HeldItem item)
        {
            heldItems.Add(item);
            item.IsBeingHeld = true;
            UpdateItemToShow();
        }
        
        private void UpdateItemToShow()
        {
            if (heldItems.Count == 0)
            {
                visibleItem = null;
                return;
            }

            if (visibleItem)
            {
                visibleItem.IsBeingHeld = false;
                visibleItem.gameObject.SetActive(false);
            }
            
            visibleItem = heldItems[^1];
            visibleItem.gameObject.SetActive(true);
            paranter.SetObjectAsChild(visibleItem.transform);
        }
    }
}