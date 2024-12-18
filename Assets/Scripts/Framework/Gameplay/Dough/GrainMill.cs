using System;
using Framework.Gameplay.HeldItemSystem;
using UnityEngine;

namespace Framework.Gameplay.Dough
{
    public sealed class GrainMill : Interactable
    {
        [SerializeField] private Timer gridingTimer;
        [SerializeField] private ItemHolder itemHolder;
        [SerializeField] private Paranter paranter;
        [SerializeField] private Flower give;

        private InteractionState _currentState;
        
        public override void DoInteraction()
        {
            switch (_currentState)
            {
                case InteractionState.EMPTY:
                    HeldItem grain = itemHolder.GetItemFormHolder(HeldItemType.GRAIN);
                    paranter.SetObjectAsChild(grain.transform);
                    gridingTimer.StartCounting();
                    _currentState = InteractionState.DOING;
                    break;
                case InteractionState.DOING:
                    paranter.GetChild().gameObject.SetActive(false);
                    itemHolder.HoldItem(give); // todo: bad version
                    _currentState = InteractionState.EMPTY;
                    break;
                case InteractionState.DONE:
                    paranter.GetChild().gameObject.SetActive(false);
                    itemHolder.HoldItem(give);
                    _currentState = InteractionState.EMPTY;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}