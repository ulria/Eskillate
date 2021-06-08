using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class HighScoreHelper
    {
        private static HighScores _highScores { get; set; }
        private static List<string> _starsSpritePaths = new List<string>() {
            "Core\\Score\\0stars",
            "Core\\Score\\1star",
            "Core\\Score\\2stars",
            "Core\\Score\\3stars"
        };

        [Serializable]
        private class HighScores
        {
            public List<LevelHighScore> LevelHighScores;
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
            // TODO - Add protection if the score does not exist yet. Not sure if this is a good idea, or if we require to change the HighScore file every time we add a level.
            highScore.First().Score = score;
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

            var json = ReadHighScoresFile();
            _highScores = UnityEngine.JsonUtility.FromJson<HighScores>(json);
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
            string path = "Assets/Resources/Core/HighScores.json";
            var json = File.ReadAllText(path);
            return json;
        }

        private static void WriteHighScoresFile(string content)
        {
            string path = "Assets/Resources/Core/HighScores.json";

            File.WriteAllText(path, content);
        }

        public static Sprite LoadStarsSprite(Stars score)
        {
            var clampedScore = Mathf.Clamp((int)score, 0, 3);
            return Resources.Load<Sprite>(_starsSpritePaths[clampedScore]);
        }
    }
}