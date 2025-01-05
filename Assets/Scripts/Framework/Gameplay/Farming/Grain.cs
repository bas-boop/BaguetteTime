using System.Collections;
using UnityEngine;

using Framework.Extensions;
using Framework.Gameplay.HeldItemSystem;
using Tools;

namespace Framework.Gameplay.Farming
{
    [DefaultExecutionOrder(0)]
    public sealed class Grain : HeldItem
    {
        [SerializeField] private float growHeight = 1;
        [SerializeField] private float growSpeed = 1;
        [SerializeField] private Gradient growColor;
        [SerializeField] private Material startMat;
        [SerializeField] private GameObject[] mats;

        public float CurrentEvaluate { get; private set; }

        private bool _isGrowing;
        private Vector3 _startPosition;
        private Vector3 _finalPosition;
        private Material _runtimeMaterial;

        private void Start()
        {
            _startPosition = transform.position;
            _finalPosition = transform.position;
            _startPosition.SubtractY(growHeight);
            transform.position = _startPosition;
        }

        private void OnDestroy()
        {
            if (_runtimeMaterial != null) 
                Destroy(_runtimeMaterial);
        }

        public void StartGrowing()
        {
            gameObject.SetActive(true);

            if (_isGrowing)
                return;
            
            _isGrowing = true;
            StartCoroutine(MoveToPosition(_finalPosition, growSpeed));
        }

        private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            _runtimeMaterial = MaterialCreationTool.CreateMaterialCopy(startMat);
            _runtimeMaterial.color = growColor.Evaluate(0);
            UpdateMats(_runtimeMaterial);

            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                CurrentEvaluate = elapsedTime / duration;
                _runtimeMaterial.color = growColor.Evaluate(CurrentEvaluate);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _runtimeMaterial.color = growColor.Evaluate(CurrentEvaluate);
            CurrentEvaluate = 1;
            transform.position = targetPosition;
        }

        /// <summary>
        /// temp function, I hope
        /// Because of developer art, when good art exist this should be removed or changed.
        /// </summary>
        private void UpdateMats(Material m)
        {
            foreach (GameObject meshObject in mats)
            {
                if (meshObject.TryGetComponent(out MeshRenderer renderer)) 
                    renderer.material = m;
            }
        }
    }
}
