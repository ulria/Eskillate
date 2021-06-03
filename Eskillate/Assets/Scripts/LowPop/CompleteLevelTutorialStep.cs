using UnityEngine;

namespace LowPop
{
    public class CompleteLevelTutorialStep : TutorialStep
    {
        public override void Load()
        {
            Debug.Log($"{System.DateTime.Now} CompleteLevelTutorialStep loaded.");

            _tutorialManager.CompleteStep();
        }

        public override void Update()
        {
            
        }

        public override void Unload()
        {
            Debug.Log($"{System.DateTime.Now} CompleteLevelTutorialStep unloaded.");
        }
    }
}