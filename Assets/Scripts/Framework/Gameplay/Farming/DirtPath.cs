using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay.Farming
{
    [DefaultExecutionOrder(1)]
    public sealed class DirtPath : Interactable
    {
        [SerializeField] private Grain grain;
        [SerializeField] private Timer growTimer;
        [SerializeField] private UnityEvent grownd;

        private bool _isPlanted;
        private bool _isHarvested;
        
        private void Start() => grain.gameObject.SetActive(false);

        public override void DoInteraction()
        {
            if(_isHarvested)
                return;
            
            if (!_isPlanted)
            {
                _isPlanted = true;
                grain.StartGrowing();
                growTimer.StartCounting();
                return;
            }

            _isHarvested = true;
            grain.StopAllCoroutines();
            grownd?.Invoke();
        }
    }
}