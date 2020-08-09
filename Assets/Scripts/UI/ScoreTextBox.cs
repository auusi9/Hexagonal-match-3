using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreTextBox : MonoBehaviour
    {
        [SerializeField] private ScoreSystem _scoreSystem;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _scoreSystem.NewScore += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _text.SetText(score.ToString());
        }

        private void OnDestroy()
        {
            _scoreSystem.NewScore -= UpdateScore;
        }
    }
}