using UnityEngine;

namespace LowPop
{
    public class ComputeValuesTutorialStep : TutorialStep
    {
        public override void Load()
        {
            Debug.Log("ComputeValuesTutorialStep loaded.");

            _tutorialManager.CompleteStep();
        }

        public override void Update()
        {

        }
    }
}