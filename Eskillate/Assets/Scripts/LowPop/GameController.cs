﻿using UnityEngine;
using Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Label = LabelHelper.Label;
using System;
using static Core.ResourcesHelper;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace LowPop
{
    public class GameController : MonoBehaviour, IGameController
    {
        private List<Level> _levels;
        private int _loadedLevelId;
        private List<Poppable> _poppables;
        private Dictionary<string, Action> _onPoppedCallbacks = new Dictionary<string, Action>();
        private int _subscriberIndex = 0;
        private ScoreTimer _scoreTimer;
        private int _timePenalty;
        private GameObject _timerGO;
        private int _nbCorrectPops = 0;
        private int _nbPoppablesInThisLevel = 0;

        // Start is called before the first frame update
        void Start()
        {
            var text = LabelHelper.ResolveLabel(LabelHelper.Label.LowPopDirective);

            _levels = new List<Level>();

            var tutorialLevel = new TutorialLevel()
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 0,
                NameLabel = Label.LowPopTutorialName,
                DescriptionLabel = Label.LowPopTutorialDescription
            };

            var level1 = new Level(3 , Level.Difficulty.NormalOnly)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 1,
                NameLabel = Label.LowPopLevel1Name,
                DescriptionLabel = Label.LowPopLevel1Description
            };
            
            var level2 = new Level(5, Level.Difficulty.IntArithmetics)
            {
                MiniGameId = MiniGameId.LowPop,
                LevelId = 2,
                NameLabel = Label.LowPopLevel2Name,
                DescriptionLabel = Label.LowPopLevel2Description
            };

            _levels.Add(tutorialLevel);
            _levels.Add(level1);
            _levels.Add(level2);

            LoadLevelHighScores();

            LoadHelper.LoadSceneAdditively(this, "LevelSelectionMenu", FillLevelSelectionMenu);

            // Add menus
            LoadHelper.LoadGenericMenus(this);

            _timerGO = GameObject.Find("TimerGO");
        }

        private void LoadLevelHighScores()
        {
            var highScores = HighScoreHelper.GetHighScores(MiniGameId.LowPop);
            foreach (var level in _levels)
            {
                int highScore;
                highScores.TryGetValue(level.LevelId, out highScore);
                level.HighScore = highScore;
            }
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
                    currentLevelTrio = Instantiate(Resources.Load(Resource.Core.Prefabs.LevelSelection.LevelTrio)) as GameObject;
                    currentLevelTrio.transform.SetParent(scrollListContent.transform, false);
                }
                var levelGO = currentLevelTrio.transform.GetChild(levelCountForThisLevelTrio);
                var scoreGO = levelGO.GetChild(0);
                scoreGO.GetComponent<TMPro.TextMeshProUGUI>().text = level.HighScore.ToString();
                var nameGO = levelGO.GetChild(1);
                nameGO.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(level.NameLabel);
                var starsGO = levelGO.GetChild(2);
                starsGO.GetComponent<Image>().sprite = HighScoreHelper.LoadStarsSprite(level.GetStarsCount());
                var selectButtonGO = levelGO.GetChild(3);
                selectButtonGO.GetComponent<Button>().onClick.AddListener(delegate { OnLevelSelected(level.LevelId); });
                selectButtonGO.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(Label.GenericLevelPlayButton);

                levelCountForThisLevelTrio++;
            }

            while (levelCountForThisLevelTrio != 3)
            {
                GameObject.Destroy(currentLevelTrio.transform.GetChild(levelCountForThisLevelTrio).gameObject);
                levelCountForThisLevelTrio++;
            }

            // Set Y pos to 1 to be scrolled to the top
            scrollListContent.transform.parent.transform.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
        }

        private void OnLevelSelected(int levelId)
        {
            UnloadLevel();
            LoadLevel(levelId);
            GameObject.Find("LevelSelectionCanvas").SetActive(false);
        }

        private void UnloadLevel()
        {
            _levels[_loadedLevelId].Unload();
        }

        // Update is called once per frame
        void Update()
        {
            if (_scoreTimer != null)
            {
                _scoreTimer.Update();
            }   
        }

        public void LoadLevel(int id)
        {
            var level = _levels[id];
            _poppables = level.Load();
            _loadedLevelId = id;
            _nbCorrectPops = 0;
            _nbPoppablesInThisLevel = _poppables.Count;

            var allocatedTime = level.GetGracePeriodDelay() + (level.GetExtraTimePerPoppable().Multiply(_poppables.Count));
            _scoreTimer = new ScoreTimer(allocatedTime, _timerGO, LevelCompleted);
            _timePenalty = level.GetTimePenaltyPerErrorInSeconds();
            _scoreTimer.StartTimer();
        }

        public void RestartLevel()
        {
            _poppables = _levels[_loadedLevelId].Reload();
            _scoreTimer.ResetTimer();
            _scoreTimer.StartTimer();
        }

        public bool OnPopped(float valuePopped)
        {
            // Pop it first
            var res = _levels[_loadedLevelId].OnPopped(valuePopped);

            // if it was the correct one, call all the callbacks that were registered
            if(res)
            {
                // Clone the list to avoid infinite loops if someone subscribes in the OnPopped event
                var clonedOnPoppedCallbacks = new Dictionary<string, Action>(_onPoppedCallbacks);
                foreach (KeyValuePair<string, Action> callbackPair in clonedOnPoppedCallbacks)
                {
                    callbackPair.Value();
                }

                _nbCorrectPops++;
            }
            else
            {
                _scoreTimer.ApplyTimePenalty(_timePenalty);
            }

            return res;
        }

        public void LevelCompleted()
        {
            Debug.Log($"{System.DateTime.Now} Level Completed");
            var levelCompletionGO = GameObject.FindGameObjectWithTag("LevelCompletionMenu");
            var level = _levels[_loadedLevelId];
            _scoreTimer.StopTimer();
            Debug.Log($"_nbPoppablesInThisLevel {_nbPoppablesInThisLevel} - _nbCorrectPops {_nbCorrectPops}");
            float ratioOfCorrectPops = (float)_nbCorrectPops / _nbPoppablesInThisLevel;
            float finalScore = level.Score * ratioOfCorrectPops;
            Debug.Log($"finalScore {finalScore} - level.Score {level.Score} - ratioOfCorrectPops {ratioOfCorrectPops}");
            levelCompletionGO.GetComponent<LevelCompletionMenu>().OnLevelCompleted(level, (int)finalScore);
        }

        public Poppable GetNextPoppableToPop()
        {
            return _levels[_loadedLevelId].GetNextPoppableToPop();
        }

        private string GetNextSubscriberId()
        {
            return $"subscriber-{_subscriberIndex++}";
        }

        public string SubscribeToPopping(Action callback)
        {
            var subscriberId = GetNextSubscriberId();
            _onPoppedCallbacks.Add(subscriberId, callback);

            return subscriberId;
        }

        public void Unsubscribe(string subscriberId)
        {
            _onPoppedCallbacks.Remove(subscriberId);
        }
    }
}