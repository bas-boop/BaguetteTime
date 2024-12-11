using System.Collections;
using UnityEngine;

using Framework.Extensions;
using Tools;

namespace Framework.Gameplay.Farming
{
    [DefaultExecutionOrder(0)]
    public sealed class Grain : MonoBehaviour
    {
        [SerializeField] private float growHeight = 1;
        [SerializeField] private float growSpeed = 1;
        [SerializeField] private Gradient growColor;
        [SerializeField] private Material startMat;
        [SerializeField] private GameObject[] mats;

        private bool _isGrowing;
        private string _matPath;
        
        private Vector3 _startPosition;
        private Vector3 _finalPosition;
        
        private void Start()
        {
            _startPosition = transform.position;
            _finalPosition = transform.position;
            _startPosition.SubtractY(growHeight);
            transform.position = _startPosition;
        }

        private void OnDestroy()
        {
            MaterialCreationTool.DeleteMaterial(_matPath);
        }

        public void StartGrowing()
        {
            gameObject.SetActive(true);

            if (!_isGrowing)
            {
                _isGrowing = true;
                StartCoroutine(MoveToPosition(_finalPosition, growSpeed));
            }
        }

        private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            (Material currentMat, string path) = MaterialCreationTool.CreateAndReturnMaterialCopy(startMat);
            _matPath = path;
            
            currentMat.color = growColor.Evaluate(0);

            UpdateMats(currentMat);
            
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                currentMat.color = growColor.Evaluate(elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            currentMat.color = growColor.Evaluate(1);
            transform.position = targetPosition;
        }

        /// <summary>
        /// Temp function
        /// </summary>
        private void UpdateMats(Material m)
        {
            foreach (GameObject gameObjct in mats)
            {
                gameObjct.GetComponent<MeshRenderer>().material = m;
            }
        }
    }
}