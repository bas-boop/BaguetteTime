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

        private PlantingState _currentState;
        
        private void Start() => grain.gameObject.SetActive(false);

        public override void DoInteraction()
        {
            switch (_currentState)
            {
                case PlantingState.HARVESTED:
                    break;
                case PlantingState.NOT_PLANTED:
                    _currentState = PlantingState.PLANTED;
                    grain.StartGrowing();
                    growTimer.StartCounting();
                    break;
                case PlantingState.PLANTED:
                    _currentState = PlantingState.HARVESTED;
                    grain.StopAllCoroutines();
                    onHarvested?.Invoke();
                    break;
            }
        }
    }
}