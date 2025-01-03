using TMPro;
using UnityEngine;

using Framework.Gameplay;

namespace UI.World
{
    public sealed class ScoreText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        public void UpdateText() => text.text = $"Your score is { GetLetterGrade(Score.Instance.GetScore)}";

        private static string GetLetterGrade(float score) =>
            score switch
            {
                >= 120 => "S+",
                >= 101 and < 120 => "S",
                >= 90 and <= 100 => "A",
                >= 70 and <= 89 => "B",
                >= 60 and <= 69 => "C",
                >= 50 and <= 59 => "D",
                >= 0 and <= 49 => "F",
                _ => "Invalid score"
            };
    }
}