﻿using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Core.ResourcesHelper;

namespace Core
{
    public class HighScoreHelper
    {
        private static HighScores _highScores { get; set; }
        private static List<string> _starsSpritePaths = new List<string>() {
            Resource.Core.Score.star0,
            Resource.Core.Score.star1,
            Resource.Core.Score.star2,
            Resource.Core.Score.star3
        };

        [Serializable]
        private class HighScores
        {
            public List<LevelHighScore> LevelHighScores = new List<LevelHighScore>();
        }

        [Serializable]
        private class LevelHighScore
        {
            public MiniGameId MiniGameId;
            public int LevelId;
            public int Score;
        }

        public static void SaveNewHighScore(MiniGameId miniGameId, int levelId, int score)
        {
            var highScores = LoadHighScores();
            var highScore = highScores.LevelHighScores.Where(hs => hs.MiniGameId == miniGameId && hs.LevelId == levelId);
            if (highScore.Count() < 1)
            {
                var newHighScore = new LevelHighScore();
                newHighScore.MiniGameId = miniGameId;
                newHighScore.LevelId = levelId;
                newHighScore.Score = score;
                highScores.LevelHighScores.Add(newHighScore);
            }
            else
            {
                highScore.First().Score = score;
            }
            SaveHighScores();
        }
        
        public static Dictionary<int, int> GetHighScores(MiniGameId miniGameId)
        {
            var dict = new Dictionary<int, int>();

            var highScores = LoadHighScores();
            var miniGameHighScores = highScores.LevelHighScores.Where(hs => hs.MiniGameId == miniGameId);
            foreach (var highScore in miniGameHighScores)
            {
                dict.Add(highScore.LevelId, highScore.Score);
            }
            return dict;
        }

        private static HighScores LoadHighScores()
        {
            if (_highScores != null)
                return _highScores;

            try
            {
                var json = ReadHighScoresFile();
                _highScores = UnityEngine.JsonUtility.FromJson<HighScores>(json);
            }
            catch (Exception e)
            {
                // there were no HighScores file
                _highScores = new HighScores();
            }
            
            return _highScores;
        }

        private static void SaveHighScores()
        {
            _highScores = LoadHighScores();
            var json = UnityEngine.JsonUtility.ToJson(_highScores);
            WriteHighScoresFile(json);
        }

        private static string ReadHighScoresFile()
        {
            var json = File.ReadAllText(Resource.PersistentData.HighScores);
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("Could not find HighScores file ("+ Resource.PersistentData.HighScores + ").");
            }
            return json;
        }

        private static void WriteHighScoresFile(string content)
        {
            File.WriteAllText(Resource.PersistentData.HighScores, content);
        }

        public static Sprite LoadStarsSprite(Stars score)
        {
            var clampedScore = Mathf.Clamp((int)score, (int)Stars.NONE, (int)Stars.THREE);
            return Resources.Load<Sprite>(_starsSpritePaths[clampedScore]);
        }
    }
}