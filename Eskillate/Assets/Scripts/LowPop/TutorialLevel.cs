using Core;
using System.Collections.Generic;
using System.Linq;
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
                poppables[poppableIndex].SetPoppingPrevented(true);
            }

            return poppables;
        }

        public override bool OnPopped(float valuePopped)
        {
            if (_activeElements.Count < 1)
            {
                // No more poppables left, so the user clicked an already popped one
                return false;
            }

            if (_activeElements.First().Value != valuePopped)
            {
                // The user clicked the wrong one
                return false;
            }
            else
            {
                _activeElements.RemoveAt(0);
                return true;
            }
        }

        public override List<Poppable> Reload()
        {
            _tutorialManager.Reload();

            // Load the popables
            var poppables = base.Reload();

            var nbPoppables = poppables.Count;

            // Reallign them on a line
            var dist = SCREEN_WIDTH / (nbPoppables + 1);
            for (var poppableIndex = 0; poppableIndex < nbPoppables; poppableIndex++)
            {
                var x = (poppableIndex + 1) * dist - (SCREEN_WIDTH / 2.0f);
                var y = -200.0f;
                poppables[poppableIndex].SetPosition(new Vector2(x, y));
                poppables[poppableIndex].SetPoppingPrevented(true);
            }
            return poppables;
        }

        public override Stars GetStarsCount()
        {
            var scoreInStars = HighScore / 33.0f;
            return (Stars)scoreInStars;
        }
    }
}