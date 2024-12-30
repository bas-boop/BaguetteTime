using System.Collections;
using UnityEngine;

using Framework.Gameplay.HeldItemSystem;

namespace Framework.Gameplay.Dough
{
    public sealed class RestingPlank : ItemTaker
    {
        [SerializeField] private Paranter paranter;
        [SerializeField] private float riseTime = 2;
        [SerializeField] private float growFactor = 0.03f;

        private bool _hasDough;
        
        public void TakeItemOrAction()
        {
            if (_hasDough)
            {
                GiveBackItem();
                _hasDough = false;
            }
            else
                TakeItem(HeldItemType.DOUGH);
        }
        
        public override void TakeAction()
        {
            paranter.SetObjectAsChild(p_takenItem.transform);
            _hasDough = true;
            Transform t = ((Dough) p_takenItem).GetVisual();
            StartCoroutine(RiseDough(t));
        }

        private IEnumerator RiseDough(Transform target)
        {
            Vector3 initialScale = target.localScale;
            Vector3 targetScale = initialScale * (1 + growFactor);
            float elapsedTime = 0;

            while (elapsedTime < riseTime)
            {
                float t = elapsedTime / riseTime;
                target.localScale = Vector3.Lerp(initialScale, targetScale, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.localScale = targetScale;
        }
    }
}