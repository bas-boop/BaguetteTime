using System;
using UnityEngine;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class GrainMill : Interactable
    {
        [SerializeField] private Timer gridingTimer;
        [SerializeField] private ItemHolder itemHolder;
        [SerializeField] private Paranter paranter;
        [SerializeField] private Flour flourPrefab;
        
        public override void DoInteraction()
        {
            switch (p_currentState)
            {
                case InteractionState.EMPTY:
                    HeldItem grain = itemHolder.GetItemFormHolder(HeldItemType.GRAIN);
                    paranter.SetObjectAsChild(grain.transform);
                    gridingTimer.StartCounting();
                    p_currentState = InteractionState.DOING;
                    break;
                
                case InteractionState.DOING:
                    paranter.GetChild().gameObject.SetActive(false);
                    Flour f = (Flour) itemHolder.CreateAndHoldItem(flourPrefab);
                    f.MakeFlowerBad();
                    p_currentState = InteractionState.EMPTY;
                    break;
                
                case InteractionState.DONE:
                    paranter.GetChild().gameObject.SetActive(false);
                    itemHolder.CreateAndHoldItem(flourPrefab);
                    p_currentState = InteractionState.EMPTY;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}