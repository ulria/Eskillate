using UnityEngine.SceneManagement;
using UnityEngine;

namespace Core
{
    public class PauseMenu : MonoBehaviour
    {
        private GameObject _pauseMenu;
        private GameObject _gameController;

        // Start is called before the first frame update
        void Start()
        {
            _pauseMenu = GameObject.Find("PauseMenuCanvas");
            _gameController = GameObject.Find("GameController");

            var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            _pauseMenu.GetComponent<Canvas>().worldCamera = mainCamera;
            var pauseButtonCanvas = GameObject.Find("PauseButtonCanvas");
            pauseButtonCanvas.GetComponent<Canvas>().worldCamera = mainCamera;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPauseButtonClicked()
        {
            _pauseMenu.SetActive(true);
        }

        public void OnRestartLevelButtonClicked()
        {
            _gameController.GetComponent<IGameController>().RestartLevel();
            _pauseMenu.SetActive(false);
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnResumeButtonClicked()
        {
            _pauseMenu.SetActive(false);
        }
    }
}