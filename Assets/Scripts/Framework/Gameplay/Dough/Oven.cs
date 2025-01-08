using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class Oven : ItemTaker
    {
        [SerializeField] private Paranter paranter;
        [SerializeField] private Timer bakeTimer;
        [SerializeField] private Transform door;
        [SerializeField] private GameObject lamp;
        [SerializeField] private float duration = 2f;

        [SerializeField] private UnityEvent onBreadBaked = new();

        private InteractionState _currentState;
        private bool _isOpen = true;
        private float _t;
        
        public override void TakeItemOrAction()
        {
            switch (_currentState)
            {
                case InteractionState.EMPTY:
                    TakeItem(HeldItemType.DOUGH);
                    break;
                
                case InteractionState.DOING:
                    bakeTimer.SetCanCount(false);
                    StopBaking();
                    Score.Instance.IncreaseScore(_t, true);
                    break;
                
                case InteractionState.DONE:
                    StopBaking();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void TakeAction()
        {
            paranter.SetObjectAsChild(p_takenItem.transform);
            StartCoroutine(TurnOvenDoor(true));
        }

        public void StopBaking()
        {
            _currentState = InteractionState.EMPTY;
            Score.Instance.IncreaseScore(_t, true);
            lamp.SetActive(false);
            StartCoroutine(TurnOvenDoor(false));
            ((Dough) p_takenItem).Bread();
            onBreadBaked?.Invoke();
        }

        private IEnumerator TurnOvenDoor(bool shouldBake)
        {
            Quaternion initialRotation = door.rotation;
            Quaternion targetRotation = initialRotation * Quaternion.Euler(_isOpen ? 90 : -90, 0, 0);
            float elapsedTime = 0f;
    
            _isOpen = !_isOpen;

            while (elapsedTime < duration)
            {
                _t = elapsedTime / duration;
                door.rotation = Quaternion.Slerp(initialRotation, targetRotation, _t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _t = 1;
            door.rotation = targetRotation;
            
            if (shouldBake)
                StartBaking();
        }

        private void StartBaking()
        {
            bakeTimer.SetCanCount(true);
            _currentState = InteractionState.DOING;
            lamp.SetActive(true);
        }
    }
}