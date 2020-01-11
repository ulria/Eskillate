using UnityEngine;
using Core;
using System.Collections.Generic;
using System.Linq;

namespace LowPop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private List<Level> _levels;
        private int _loadedLevelId;
        private List<Poppable> _poppables;

        // Start is called before the first frame update
        void Start()
        {
            _levels = new List<Level>();

            var level1 = new Level(3 , Level.Difficulty.NormalOnly)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 1,
                Name = "Level1",
                Description = "This is level 1.",
                HighScore = 0
            };

            var level2 = new Level(5, Level.Difficulty.IntArithmetics)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 2,
                Name = "Level2",
                Description = "This is level 2.",
                HighScore = 0
            };

            _levels.Add(level1);
            _levels.Add(level2);

            // TODO - Remove this as it will be called from the level selection menu
            LoadLevel(0);

            // Add menus
            LoadHelper.LoadGenericMenus(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadLevel(int id)
        {
            _poppables = _levels[id].Load();
            _loadedLevelId = id;
        }

        public void RestartLevel()
        {
            _poppables = _levels[_loadedLevelId].Reload();
            var poppableGameObjects = GameObject.FindGameObjectsWithTag("Poppable");
            foreach(var poppableGameObject in poppableGameObjects)
            {
                var renderers = poppableGameObject.GetComponentsInChildren<Renderer>();
                foreach(var renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
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
            var levelCompletionGO = GameObject.FindGameObjectWithTag("LevelCompletionMenu");
            levelCompletionGO.GetComponent<LevelCompletionMenu>().OnLevelCompleted(_levels[_loadedLevelId], 100);
        }
    }
}