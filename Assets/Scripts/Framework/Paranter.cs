using UnityEngine;

namespace Framework
{
    public sealed class Paranter : MonoBehaviour
    {
        private Transform _child;
        
        public void SetObjectAsChild(Transform targetChild)
        {
            targetChild.SetParent(transform);
            targetChild.localPosition = Vector3.zero;
            targetChild.localEulerAngles = Vector3.zero;
            targetChild.localScale = Vector3.one;

            _child = targetChild;
        }

        public Transform GetChild() => _child;

        public void DeleteChild()
        {
            if (!_child)
                return;
            
            Destroy(_child.gameObject);
            _child = null;
        }
    }
}