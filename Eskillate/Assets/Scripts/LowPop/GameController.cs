using UnityEngine;
using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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

            var highScores = HighScoreHelper.GetHighScores(MiniGameId.LowPop);
            var level1 = new Level(3 , Level.Difficulty.NormalOnly)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 1,
                Name = "Level1",
                Description = "This is level 1.",
                HighScore = highScores[1]
            };

            var level2 = new Level(5, Level.Difficulty.IntArithmetics)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 2,
                Name = "Level2",
                Description = "This is level 2.",
                HighScore = highScores[2]
            };

            _levels.Add(level1);
            _levels.Add(level2);

            LoadHelper.LoadSceneAdditively(this, "LevelSelectionMenu", FillLevelSelectionMenu);

            // Add menus
            LoadHelper.LoadGenericMenus(this);
        }

        private void FillLevelSelectionMenu()
        {
            var levelSelectionCanvas = GameObject.Find("LevelSelectionCanvas");
            var scrollListContent = levelSelectionCanvas.transform.Find("ScrollList").transform.Find("ScrollListViewport").transform.Find("ScrollListContent");

            foreach (Transform child in scrollListContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            int levelCountForThisLevelTrio = 0;
            GameObject currentLevelTrio = new GameObject();
            foreach (var level in _levels)
            {
                levelCountForThisLevelTrio = levelCountForThisLevelTrio % 3;
                if (levelCountForThisLevelTrio == 0)
                {
                    currentLevelTrio = Instantiate(Resources.Load("Core\\Prefabs\\LevelSelection\\LevelTrio")) as GameObject;
                    currentLevelTrio.transform.SetParent(scrollListContent.transform, false);
                }
                var levelGO = currentLevelTrio.transform.GetChild(levelCountForThisLevelTrio);
                var scoreGO = levelGO.GetChild(0);
                scoreGO.GetComponent<TMPro.TextMeshProUGUI>().text = level.HighScore.ToString();
                var nameGO = levelGO.GetChild(1);
                nameGO.GetComponent<TMPro.TextMeshProUGUI>().text = level.Name;
                var starsGO = levelGO.GetChild(2);
                var selectButtonGO = levelGO.GetChild(3);
                selectButtonGO.GetComponent<Button>().onClick.AddListener(delegate { OnLevelSelected(level.LevelId); });

                levelCountForThisLevelTrio++;
            }

            while (levelCountForThisLevelTrio != 3)
            {
                GameObject.Destroy(currentLevelTrio.transform.GetChild(levelCountForThisLevelTrio).gameObject);
                levelCountForThisLevelTrio++;
            }

            // Set Y pos to 1 to be at scrolled to the top
            scrollListContent.transform.parent.transform.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
        }

        private void OnLevelSelected(int levelId)
        {
            UnloadLevel();
            LoadLevel(levelId - 1);
            GameObject.Find("LevelSelectionCanvas").SetActive(false);
        }

        private void UnloadLevel()
        {
            _levels[_loadedLevelId].Unload();
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