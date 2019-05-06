using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

namespace DragAndDrop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private int _correctlyPlacedCounter = 0;
        private MoveableShape[] _shapeArray;

        // Start is called before the first frame update
        void Start()
        {
            _shapeArray = FindObjectsOfType<MoveableShape>();

            // Add PauseMenu
            StartCoroutine(LoadAdditiveScene.LoadAsync("PauseMenu"));
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