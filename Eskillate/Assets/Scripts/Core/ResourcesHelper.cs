using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class ResourcesHelper
    {
        public struct Resource
        {
            public struct Core
            {
                public static string Path = "Core";
                public struct Score
                {
                    public static string Path = Core.Path + "\\Score";
                    public static string star0 = Path + "\\0stars";
                    public static string star1 = Path + "\\1stars";
                    public static string star2 = Path + "\\2stars";
                    public static string star3 = Path + "\\3stars";
                };
                public struct LevelCompletionMenu
                {
                    public static string Path = Core.Path + "\\LevelCompletionMenu";
                    public static string MainMenuButton = Path + "\\mainMenuButton";
                    public static string RestartButton = Path + "\\restartButton";
                };
                public struct MiniGame
                {
                    public static string Path = Core.Path + "\\MiniGame";
                    public struct Clips
                    {
                        public static string Path = MiniGame.Path + "\\Clips";
                        public static string DragAndDrop = Path + "\\DragAndDrop";
                        public static string ReproduceShape = Path + "\\ReproduceShape";
                        public static string ReproduceSequence = Path + "\\ReproduceSequence";
                        public static string HideAndSeek = Path + "\\HideAndSeek";
                        public static string LowPop = Path + "\\LowPop";
                        public static string PerilousPath = Path + "\\PerilousPath";
                        public static string TrueColor = Path + "\\TrueColor";
                        public static string Sort = Path + "\\Sort";
                    }
                    public struct Images
                    {
                        public static string Path = MiniGame.Path + "\\Images";
                        public static string DragAndDrop = Path + "\\DragAndDrop";
                        public static string ReproduceShape = Path + "\\ReproduceShape";
                        public static string ReproduceSequence = Path + "\\ReproduceSequence";
                        public static string HideAndSeek = Path + "\\HideAndSeek";
                        public static string LowPop = Path + "\\LowPop";
                        public static string PerilousPath = Path + "\\PerilousPath";
                        public static string TrueColor = Path + "\\TrueColor";
                        public static string Sort = Path + "\\Sort";
                    }
                }
                public struct Prefabs
                {
                    public static string Path = Core.Path + "\\Prefabs";
                    public struct MiniGameSelection
                    {
                        public static string Path = Prefabs.Path + "\\MiniGameSelection";
                        public static string MiniGame = Path + "\\MiniGame";
                    }
                    public struct LevelSelection
                    {
                        public static string Path = Prefabs.Path + "\\LevelSelection";
                        public static string LevelTrio = Path + "\\LevelTrio";
                    }
                }
                public struct Labels
                {
                    public static string Path = Core.Path + "\\Labels";
                }
                public static string GradientAlphaMaterial = Path + "\\GradientAlphaMaterial";
                public static string GradientShapeMaterial = Path + "\\GradientShapeMaterial";
            }
            public struct LowPop
            {
                public static string Path = "LowPop";
                public static string Square = Path + "\\Square";
                public static string Circle1 = Path + "\\Circle1";
            }
            public struct FontsAndMaterials
            {
                public static string Path = "Fonts & Materials";
                public static string LiberationSansSDFDropShadow = Path + "\\LiberationSans SDF - Drop Shadow";
            }
            public struct PersistentData
            {
                public static string Path = Application.persistentDataPath;
                public static string HighScores = Path + "\\HighScores.json";
            }
        }
    }
}
