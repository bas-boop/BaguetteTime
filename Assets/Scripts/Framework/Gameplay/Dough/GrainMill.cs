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

        private InteractionState _currentState;
        
        public override void DoInteraction()
        {
            Debug.Log(_currentState);
            
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
                    Flour f = (Flour) itemHolder.CreateAndHoldItem(flourPrefab);
                    f.MakeFlowerBad();
                    _currentState = InteractionState.EMPTY;
                    break;
                
                case InteractionState.DONE:
                    paranter.GetChild().gameObject.SetActive(false);
                    itemHolder.CreateAndHoldItem(flourPrefab);
                    _currentState = InteractionState.EMPTY;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetStateToDone() => _currentState = InteractionState.DONE;
    }
}