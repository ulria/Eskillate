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
        private Action _callbackOnExpired;
        private bool _is_started = false;

        public ScoreTimer(TimeSpan allocatedTime, GameObject go, Action callbackOnExpired)
        {
            _allocatedTime = allocatedTime;
            _timerGO = go;
            _callbackOnExpired = callbackOnExpired;
            _stopWatch = new Stopwatch();
        }

        public void StartTimer()
        {
            _stopWatch.Start();
            _is_started = true;
        }

        public void ResetTimer()
        {
            _stopWatch.Reset();
            _penalty = new TimeSpan(0, 0, 0);
        }

        public void StopTimer()
        {
            _is_started = false;
            _stopWatch.Stop();
        }
        
        public void Update()
        {
            if (_is_started)
            {
                var timeRemaining = GetTimeRemaining();

                var textGO = _timerGO.transform.GetChild(0);
                var textElement = textGO.GetComponent<TMPro.TextMeshPro>();
                textElement.text = string.Format("{0:N2}", timeRemaining.TotalSeconds);
                if (timeRemaining.TotalSeconds < 0)
                {
                    _callbackOnExpired();
                }
                else
                {
                    // Display time remaining in a color gradient from green (timeRemaining / _allocatedTime == 1) to red (timeRemaining / _allocatedTime == 0)
                    // green stays at one until ratio == 0.5, then gradually drops to 0
                    // red gradually ramps to 1 until ratio == 0.5 then stays at 1
                    float green = (float)(2*timeRemaining.TotalSeconds / _allocatedTime.TotalSeconds);
                    float red = 2.0f - (float)(2*timeRemaining.TotalSeconds / _allocatedTime.TotalSeconds);
                    textElement.color = new Color(red, green, 0.0f, 1);
                }
            }
        }

        public void ApplyTimePenalty(int seconds)
        {
            var secondsAsTimeSpan = new TimeSpan(0, 0, seconds);
            _penalty += secondsAsTimeSpan;
        }

        public TimeSpan GetTimeRemaining()
        {
            return _allocatedTime - _stopWatch.Elapsed - _penalty;
        }
    }
}
