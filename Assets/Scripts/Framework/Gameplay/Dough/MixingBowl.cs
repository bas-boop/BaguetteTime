using System;
using UnityEngine;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class MixingBowl : Interactable
    {
        [SerializeField] private InteractManager interactManager;
        [SerializeField] private HeldItem doughPrefab;
        [SerializeField] private Timer mixTimer;
        [SerializeField] private ItemHolder itemHolder;
        [SerializeField] private Paranter one;
        [SerializeField] private Paranter two;
        [SerializeField] private Transform turn;
        [SerializeField] private float turnSpeed = 1;
        [SerializeField] private HeldItemType[] mixableItems;
        
        private MixingState _currentMixingState;
        private HeldItem _dough;

        private void Update()
        {
            if (p_currentState == InteractionState.DOING)
                turn.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }

        public override void DoInteraction()
        {
            switch (p_currentState)
            {
                case InteractionState.EMPTY:
                    AddIngredient();
                    break;
                case InteractionState.DOING:
                    if (!_dough)
                        SpawnDough();
                    
                    mixTimer.SetCanCount(false);
                    Score.Instance.IncreaseScore(mixTimer.GetCurrentTimerPercentage(), true);
                    Done();
                    break;
                case InteractionState.DONE:
                    Score.Instance.IncreaseScore(null);
                    Done();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SpawnDough()
        {
            _dough = Instantiate(doughPrefab, turn.position, Quaternion.identity, transform);
            p_currentState = InteractionState.EMPTY;
            _currentMixingState = MixingState.HAS_NOTHING;
        }

        public void DeleteIngredients()
        {
            one.DeleteChild();
            two.DeleteChild();
        }

        private void AddIngredient()
        {
            HeldItem heldItem;
            
            switch (_currentMixingState)
            {
                case MixingState.HAS_NOTHING:
                    heldItem = itemHolder.GetItemFormHolder(HeldItemType.WATER);

                    if (heldItem == null)
                    {
                        heldItem = itemHolder.GetItemFormHolder(HeldItemType.FLOUR);

                        if (heldItem != null)
                        {
                            _currentMixingState = MixingState.HAS_FLOUR;
                            two.SetObjectAsChild(heldItem.transform);
                        }
                    }
                    else
                    {
                        _currentMixingState = MixingState.HAS_WATER;
                        one.SetObjectAsChild(heldItem.transform);
                    }
                    
                    break;
                
                case MixingState.HAS_FLOUR:
                    heldItem = itemHolder.GetItemFormHolder(HeldItemType.WATER);
                    
                    if (heldItem == null)
                        break;
                    
                    _currentMixingState = MixingState.HAS_BOTH;
                    one.SetObjectAsChild(heldItem.transform);
                    interactManager.CheckDistance(this);
                    break;
                
                case MixingState.HAS_WATER:
                    heldItem = itemHolder.GetItemFormHolder(HeldItemType.FLOUR);
                    
                    if (heldItem == null)
                        break;
                    
                    _currentMixingState = MixingState.HAS_BOTH;
                    two.SetObjectAsChild(heldItem.transform);
                    interactManager.CheckDistance(this);
                    break;
                
                case MixingState.HAS_BOTH:
                    StartMixing();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartMixing()
        {
            p_currentState = InteractionState.DOING;
            mixTimer.StartCounting();
        }

        private void Done()
        {
            DeleteIngredients();
            itemHolder.HoldItem(_dough);
            interactManager.CheckDistance(this);
        }
    }
}