using UnityEngine;

namespace Framework
{
    public sealed class Paranter : MonoBehaviour
    {
        public void SetObjectAsChild(Transform targetChild)
        {
            targetChild.SetParent(transform);
            targetChild.localPosition = Vector3.zero;
            targetChild.localEulerAngles = Vector3.zero;
            targetChild.localScale = Vector3.one;
        }
    }
}