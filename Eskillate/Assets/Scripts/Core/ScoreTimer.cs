using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Core
{
    class ScoreTimer
    {
        private GameObject _timerGO;
        private Stopwatch _stopWatch;
        private TimeSpan _allocatedTime;
        private TimeSpan _penalty;

        public ScoreTimer()
        {
            _stopWatch = new Stopwatch();
        }

        public void StartTimer()
        {
            _stopWatch.Start();
        }

        public void ResetTimer()
        {
            _stopWatch.Reset();
            _penalty = new TimeSpan(0, 0, 0);
        }

        public void StopTimer()
        {
            _stopWatch.Stop();
        }

        public void SetAllocatedTime(TimeSpan allocatedTime)
        {
            _allocatedTime = allocatedTime;
        }

        public void Update()
        {
            var timeRemaining = GetTimeRemaining();
            
            var textGO = _timerGO.transform.GetChild(0);
            var textElement = textGO.GetComponent<TMPro.TextMeshPro>();
            textElement.text = string.Format("{0:N2}", timeRemaining.TotalSeconds);
            if (timeRemaining.TotalSeconds < 0)
            {
                // Display time remaining in red
                textElement.color = new Color(225f / 255f, 0f / 255f, 0f / 255f, 1);
            }
            else
            {
                // Display time remaining in green
                textElement.color = new Color(0f / 255f, 225f / 255f, 0f / 255f, 1);
            }
        }

        public void ApplyTimePenalty(int seconds)
        {
            var secondsAsTimeSpan = new TimeSpan(0, 0, seconds);
            _penalty += secondsAsTimeSpan;
        }

        public void SetTimerGO(GameObject go)
        {
            _timerGO = go;
        }

        public TimeSpan GetTimeRemaining()
        {
            return _allocatedTime - _stopWatch.Elapsed - _penalty;
        }
    }
}
