using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Gameplay.HeldItemSystem
{
    public sealed class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Paranter paranter;
        [SerializeField] private List<HeldItem> heldItems;
        [SerializeField, Tooltip("There could only be 1 held item shown.")] private GameObject visibleItem;

        public void HoldItem(HeldItem targetItem)
        {
            if (targetItem.IsBeingHeld)
                return;
            
            heldItems.Add(targetItem);
            targetItem.IsBeingHeld = true;
            UpdateItemToShow();
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

        public HeldItem GetItemFormHolder(HeldItem targetItem)
        {
            if (!heldItems.Contains(targetItem))
                return null;
            
            targetItem.gameObject.SetActive(true);
            heldItems.Remove(targetItem);
            return targetItem;

        }

        public HeldItem GetItemFormHolder(HeldItemType targetItem)
        {
            foreach (HeldItem item in heldItems.Where(item => item.Type == targetItem))
            {
                item.gameObject.SetActive(true);
                heldItems.Remove(item);
                return item;
            }

            return null;
        }

        private void UpdateItemToShow()
        {
            if (heldItems.Count == 0)
            {
                visibleItem = null;
                return;
            }
            
            if(visibleItem)
                visibleItem.SetActive(false);
            
            visibleItem = heldItems[^1].gameObject;
            paranter.SetObjectAsChild(visibleItem.transform);
        }
    }
}