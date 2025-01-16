using UnityEngine;

namespace Environment
{
    public class RandomYRotation : MonoBehaviour
    {
        private void Start()
        {
            float randomY = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, randomY, 0f);
        }
    }
}