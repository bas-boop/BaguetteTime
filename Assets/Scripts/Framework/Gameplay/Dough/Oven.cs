using System;
using System.Collections;
using UnityEngine;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class Oven : ItemTaker
    {
        [SerializeField] private Paranter paranter;
        [SerializeField] private Timer bakeTimer;
        [SerializeField] private Transform door;
        [SerializeField] private float duration = 2f;

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
                    StartCoroutine(TurnOvenDoor(false));
                    break;
                
                case InteractionState.DONE:
                    Score.Instance.IncreaseScore(_t, true);
                    StartCoroutine(TurnOvenDoor(false));
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
            _currentState = InteractionState.DONE;
            ((Dough) p_takenItem).Bread();
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
        }
    }
}