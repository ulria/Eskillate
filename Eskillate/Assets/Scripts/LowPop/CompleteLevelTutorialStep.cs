using UnityEngine;

namespace LowPop
{
    public class CompleteLevelTutorialStep : TutorialStep
    {
        public override void Load()
        {
            Debug.Log("CompleteLevelTutorialStep loaded.");

            _tutorialManager.CompleteStep();
        }

        public override void Reload()
        {
            Debug.Log("CompleteLevelTutorialStep reloaded.");

            _tutorialManager.CompleteStep();
        }

        public override void Update()
        {
            
        }
    }
}