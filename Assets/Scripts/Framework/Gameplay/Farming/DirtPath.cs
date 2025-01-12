using UnityEngine;
using UnityEngine.Events;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Farming
{
    [DefaultExecutionOrder(1)]
    public sealed class DirtPath : Interactable
    {
        [SerializeField] private ItemHolder holder;
        [SerializeField] private Grain grain;
        [SerializeField] private Timer growTimer;
        [SerializeField] private UnityEvent onHarvested;

        private InteractionState _currentState;
        private bool _isHarvested;
        
        private void Start() => grain.gameObject.SetActive(false);

        public override void DoInteraction()
        {
            if (_isHarvested)
                return;
            
            switch (_currentState)
            {
                case InteractionState.EMPTY: // NOT PLANTED
                    _currentState = InteractionState.DOING;
                    grain.StartGrowing();
                    growTimer.StartCounting();
                    Debug.Log($"empty + {gameObject.name}");
                    break;
                
                case InteractionState.DOING: // PLANTED
                    if (HasWater())
                        break;
                    
                    _currentState = InteractionState.DONE;
                    grain.StopAllCoroutines();
                    Score.Instance.IncreaseScore(grain.CurrentEvaluate, true);
                    WhenHarvested();
                    Debug.Log($"growing + {gameObject.name}");
                    break;
                
                case InteractionState.DONE: // HARVESTED
                    HasWater();
                    Score.Instance.IncreaseScore(null);
                    WhenHarvested();
                    Debug.Log($"grown + {gameObject.name}");
                    break;
            }
        }

        private bool HasWater()
        {
            HeldItem water = holder.GetItemFormHolder(HeldItemType.WATER);

            if (!water)
                return false;
            
            Destroy(water.gameObject);
            Score.Instance.IncreaseScore(null);
            return true;
        }

        private void WhenHarvested()
        {
            _isHarvested = true;
            CanInteract = false;
            p_collider.enabled = false;
            onHarvested?.Invoke();
        }
    }
}