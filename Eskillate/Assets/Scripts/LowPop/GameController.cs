using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using System.Collections.Generic;
using System.Linq;

namespace LowPop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private List<Level> _levels;
        private List<Poppable> _poppables;

        // Start is called before the first frame update
        void Start()
        { 
            _levels = new List<Level>();

            var level1 = new Level(3 ,false)
            {
                Id = 1,
                Name = "Level1",
                Description = "This is level 1.",
                HighScore = 0
            };

            var level2 = new Level(5, true)
            {
                Id = 1,
                Name = "Level1",
                Description = "This is level 1.",
                HighScore = 0
            };

            _levels.Add(level1);
            _levels.Add(level2);

            // TODO - Remove this as it will be called from the level selection menu
            LoadLevel(0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadLevel(int id)
        {
            _poppables = _levels[id].Load();
        }

        public void RestartLevel()
        {

        }

        public bool OnPopped(float valuePopped)
        {
            if (_poppables.First().Value != valuePopped)
            {
                return false;
            }
            else
            {
                _poppables.RemoveAt(0);
                if (_poppables.Count == 0)
                    LevelCompleted();
                return true;
            }
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
    }
}