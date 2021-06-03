using UnityEngine;

namespace LowPop
{
    public abstract class TutorialStep
    {
        public abstract void Load();
        public abstract void Unload();
        public abstract void Update();
        protected TutorialManager _tutorialManager;

        public void SetTutorialManager(TutorialManager manager)
        {
            _tutorialManager = manager;
        }
    }
}