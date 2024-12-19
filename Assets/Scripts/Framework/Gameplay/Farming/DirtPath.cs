using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.Farming
{
    [DefaultExecutionOrder(1)]
    public sealed class DirtPath : Interactable
    {
        [SerializeField] private Grain grain;
        [SerializeField] private Timer growTimer;
        [SerializeField] private UnityEvent onHarvested;

        private InteractionState _currentState;
        
        private void Start() => grain.gameObject.SetActive(false);

        public override void DoInteraction()
        {
            switch (_currentState)
            {
                case InteractionState.EMPTY: // NOT PLANTED
                    _currentState = InteractionState.DOING;
                    grain.StartGrowing();
                    growTimer.StartCounting();
                    break;
                case InteractionState.DOING: // PLANTED
                    _currentState = InteractionState.DONE;
                    grain.StopAllCoroutines();
                    onHarvested?.Invoke();
                    break;
                case InteractionState.DONE: // HARVESTED
                    break;
            }
        }
    }
}