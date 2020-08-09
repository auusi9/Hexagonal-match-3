using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuitGame : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
           _button.onClick.AddListener(QuitApp);
        }

        private void QuitApp()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(QuitApp);
        }
    }
}