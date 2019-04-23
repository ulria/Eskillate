using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using System.Collections.Generic;

namespace LowPop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private List<Level> _levels;

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
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadLevel(int id)
        {
            _levels[id].Load();
        }

        public void RestartLevel()
        {

        }
    }
}