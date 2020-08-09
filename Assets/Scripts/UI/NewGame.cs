using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NewGame : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameFieldGenerator _gameField;

        private void Start()
        {
            _button.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            _gameField.RestartGame();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(RestartGame);
        }
    }
}