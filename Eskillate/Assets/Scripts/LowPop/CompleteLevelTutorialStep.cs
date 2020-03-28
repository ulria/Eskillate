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

        public override void Update()
        {
            
        }
    }
}