using System;
using System.Collections;
using UnityEngine;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class GrainMill : Interactable
    {
        [Header("References")]
        [SerializeField] private Timer gridingTimer;
        [SerializeField] private ItemHolder itemHolder;
        [SerializeField] private Paranter paranter;
        [SerializeField] private Flour flourPrefab;
        [SerializeField] private Transform grinder;
        
        [Header("Rotation settings")]
        [SerializeField] private float yRotationSpeed = 10;
        [SerializeField] private float seesawAmplitude = 2;
        [SerializeField] private float seesawSpeed = 1;

        public override void DoInteraction()
        {
            switch (p_currentState)
            {
                case InteractionState.EMPTY:
                    HeldItem grain = itemHolder.GetItemFormHolder(HeldItemType.GRAIN);
                    
                    if (!grain)
                        return;
                    
                    paranter.SetObjectAsChild(grain.transform);
                    gridingTimer.StartCounting();
                    StartCoroutine(Grind());
                    p_currentState = InteractionState.DOING;
                    break;
                
                case InteractionState.DOING:
                    paranter.GetChild().gameObject.SetActive(false);
                    Flour f = (Flour) itemHolder.CreateAndHoldItem(flourPrefab);
                    
                    if (!f)
                        return;
                    
                    f.MakeFlowerBad();
                    gridingTimer.SetCanCount(false);
                    StopAllCoroutines();
                    Score.Instance.IncreaseScore(gridingTimer.GetCurrentTimerPercentage(), true);
                    p_currentState = InteractionState.EMPTY;
                    break;
                
                case InteractionState.DONE:
                    paranter.GetChild().gameObject.SetActive(false);
                    itemHolder.CreateAndHoldItem(flourPrefab);
                    Score.Instance.IncreaseScore(null);
                    p_currentState = InteractionState.EMPTY;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void SetStateToDone()
        {
            base.SetStateToDone();
            StopAllCoroutines();
        }
        
        private IEnumerator Grind()
        {
            Quaternion initialRotation = grinder.rotation;

            while (true)
            {
                float yRotation = Time.time * yRotationSpeed;
                float xTilt = Mathf.Sin(Time.time * seesawSpeed) * seesawAmplitude;
                Quaternion newRotation = initialRotation * Quaternion.Euler(xTilt, yRotation, 0);

                grinder.rotation = newRotation;

                yield return null;
            }
        }
    }
}