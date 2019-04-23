using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

namespace DragAndDrop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private int _correctlyPlacedCounter = 0;
        private GameObject _pauseMenu;
        private MoveableShape[] _shapeArray;

        // Start is called before the first frame update
        void Start()
        {
            _shapeArray = FindObjectsOfType<MoveableShape>();

            _pauseMenu = GameObject.Find("PauseMenuCanvas");
            _pauseMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPlacedCorrectly()
        {
            if (++_correctlyPlacedCounter >= _shapeArray.Length)
                LevelCompleted();
        }

        public void OnPauseButtonClicked()
        {
            _pauseMenu.SetActive(true);
        }

        void LevelCompleted()
        {
            Debug.Log("Level Completed");
            // Call a static class and set the information, so that it is persistent to the next scene
            // LevelCompletionClass.SetMiniGameName("DragAndDrop")
            // LevelCompletionClass.SetScore("100%");
            // LevelCompletionClass.SetLevel(1);
            SceneManager.LoadScene("LevelCompletionScreen");
        }

        public void LoadLevel(int id)
        {

        }

        public void RestartLevel()
        {
            _correctlyPlacedCounter = 0;

            foreach (var moveableShape in _shapeArray)
            {
                moveableShape.Restart();
            }
        }
    }
}