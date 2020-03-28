using Core;
using System.Collections.Generic;
using UnityEngine;

namespace LowPop
{
    public class TutorialLevel : Level, ILevel
    {
        private TutorialManager _tutorialManager;

        public TutorialLevel() : base(3, Difficulty.NormalOnly)
        {
            var gameController = GameObject.Find("GameController");
            _tutorialManager = gameController.AddComponent<TutorialManager>();
        }

        public override List<Poppable> Load()
        {
            // Start the tutorial
            _tutorialManager.Init();

            // Load the popables
            var poppables = base.Load();

            var nbPoppables = poppables.Count;

            // Reallign them on a line
            var dist = SCREEN_WIDTH / (nbPoppables + 1);
            for (var poppableIndex = 0; poppableIndex < nbPoppables; poppableIndex++)
            {
                var x = (poppableIndex + 1) * dist - (SCREEN_WIDTH / 2.0f);
                var y = -200.0f;
                poppables[poppableIndex].SetPosition(new Vector2(x, y));
            }

            return poppables;
        }
    }
}