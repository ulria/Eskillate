using UnityEngine.SceneManagement;
using UnityEngine;

namespace Core
{
    public class LevelCompletionMenu : MonoBehaviour
    {
        private GameObject _levelCompletionMenu;
        private GameObject _gameController;

        public 

        // Start is called before the first frame update
        void Start()
        {
            _levelCompletionMenu = GameObject.Find("LevelCompletionMenuCanvas");
            _gameController = GameObject.Find("GameController");

            var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            _levelCompletionMenu.GetComponent<Canvas>().worldCamera = mainCamera;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnLevelCompleted(ILevel levelInfo, int score)
        {
            // Set the level info and the score in the fields
            _levelCompletionMenu.SetActive(true);
            var levelNameGO = GameObject.FindGameObjectWithTag("LevelName");
            levelNameGO.GetComponent<TMPro.TextMeshProUGUI>().text = levelInfo.Name;
            var levelDescriptionGO = GameObject.FindGameObjectWithTag("LevelDescription");
            levelDescriptionGO.GetComponent<TMPro.TextMeshProUGUI>().text = levelInfo.Description;
            var scoreGO = GameObject.FindGameObjectWithTag("Score");
            scoreGO.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("0");
            
            if (score > levelInfo.HighScore)
            {
                OnNewHighScore();
                HighScoreHelper.SaveNewHighScore(levelInfo.MiniGameId, levelInfo.LevelId, score);
                levelInfo.HighScore = score;
            }
            var highscoreGO = GameObject.FindGameObjectWithTag("Highscore");
            highscoreGO.GetComponent<TMPro.TextMeshProUGUI>().text = levelInfo.HighScore.ToString("0");
        }

        private void OnNewHighScore()
        {
            // TODO - trigger animation and sound effect.
        }

        public void OnRestartButtonClicked()
        {
            _gameController.GetComponent<IGameController>().RestartLevel();
            _levelCompletionMenu.SetActive(false);
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}