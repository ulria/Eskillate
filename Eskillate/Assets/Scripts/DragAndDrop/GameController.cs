using UnityEngine;
using Core;
using System.Collections.Generic;

namespace DragAndDrop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private int _correctlyPlacedCounter = 0;
        private MoveableShape[] _shapeArray;
        private List<Level> _levels;
        private int _loadedLevelId;

        // Start is called before the first frame update
        void Start()
        {
            _shapeArray = FindObjectsOfType<MoveableShape>();

            _levels = new List<Level>();

            var level1 = new Level()
            {
                MiniGameId = MiniGameId.DragAndDrop,
                LevelId = 1,
                Name = "Level1",
                Description = "This is level 1.",
                HighScore = 0
            };

            _levels.Add(level1);
            LoadLevel(0);

            // Add menus
            LoadHelper.LoadGenericMenus(this);
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

        public void LoadLevel(int id)
        {
            _levels[id].Load();
            _loadedLevelId = id;
        }

        public void RestartLevel()
        {
            _correctlyPlacedCounter = 0;

            foreach (var moveableShape in _shapeArray)
            {
                moveableShape.Restart();
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