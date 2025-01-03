using UnityEngine;

namespace Framework.Gameplay
{
    public sealed class Score : Singleton<Score>
    {
        [SerializeField] private float score;
        // [field: SerializeField] is not possible anymore, since Unity 6 :cry:
        [SerializeField] private float maxScoreAddAmount = 20;
        
        public float MaxScoreAddAmount => maxScoreAddAmount;

        protected override void Awake()
        {
            base .Awake();
            CanDestroyOnLoad = false;
        }

        public void IncreaseScore(float? targetAmount)
        {
            targetAmount ??= maxScoreAddAmount;
            
            if (targetAmount > maxScoreAddAmount)
                targetAmount = maxScoreAddAmount;
            
            score += (float) targetAmount;
        }
    }
}