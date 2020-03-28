using System.Collections.Generic;
using UnityEngine;

namespace LowPop
{
    public class TutorialManager : MonoBehaviour
    {
        private GameObject _gameControllerGO;
        private List<TutorialStep> _steps = new List<TutorialStep>();
        private int _currentStepIndex = 0;

        public TutorialManager()
        {
            
        }

        private void SetTutorialManagerToAllSteps()
        {
            foreach(var step in _steps)
            {
                step.SetTutorialManager(this);
            }
        }

        private void ChangeStep()
        {
            var nextStepIndex = _currentStepIndex + 1;
            if(nextStepIndex >= _steps.Count)
            {
                OnTutorialComplete();
            }
            else
            {
                _currentStepIndex = nextStepIndex;
                _steps[nextStepIndex].Load();
            }
        }

        public void CompleteStep()
        {
            ChangeStep();
        }

        private void OnTutorialComplete()
        {
            _gameControllerGO.GetComponent<GameController>().LevelCompleted();
        }
        public void Init()
        {
            _gameControllerGO = GameObject.Find("GameController");

            _steps.Clear();
            _steps.Add(new InitialDirectivesTutorialStep());
            _steps.Add(new ComputeValuesTutorialStep());
            _steps.Add(new FindLowestTutorialStep());
            _steps.Add(new RepeatFindLowestTutorialStep());
            _steps.Add(new CompleteLevelTutorialStep());
            _steps.Add(new DoneTutorialStep());

            SetTutorialManagerToAllSteps();

            _steps[0].Load();
        }

        public void Update()
        {
            foreach(var step in _steps)
            {
                step.Update();
            }
        }
    }
}