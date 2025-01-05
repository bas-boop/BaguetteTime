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

        private Vector3 _baguetteScale = new(0.3f, 0.3f, 0.9f);
        private bool _hasDough;
        private float _t;
        
        public override void TakeItemOrAction()
        {
            if (_hasDough)
            {
                GiveBackItem();
                Score.Instance.IncreaseScore(_t, true);
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
            t.localScale = _baguetteScale;
            StartCoroutine(RiseDough(t));
        }

        private IEnumerator RiseDough(Transform target)
        {
            Vector3 initialScale = target.localScale;
            Vector3 targetScale = initialScale * (1 + growFactor);
            float elapsedTime = 0;

            while (elapsedTime < riseTime)
            {
                _t = elapsedTime / riseTime;
                target.localScale = Vector3.Lerp(initialScale, targetScale, _t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _t = 1;
            target.localScale = targetScale;
        }
    }
}