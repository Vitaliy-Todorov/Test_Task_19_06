using System;
using UnityEngine;

namespace ProjectContext
{
    public class TimeController
    {
        public Action OnPause;
        public Action OnPlay;
        public Action<float> OnSetTimeSpeed;
        
        public bool IsPause { get; private set; }

        public float CurrentTime => Time.time;
        public float TimeSpeed
        {
            get => Time.timeScale;
            private set
            {
                Time.timeScale = value;
                OnSetTimeSpeed?.Invoke(value);
            }
        }

        private float _timeToPause;

        public TimeController()
        {
            SetTimeSpeed(Time.timeScale);
        }

        public void Pause()
        {
            TimeSpeed = 0;
            
            IsPause = true;
            OnPause?.Invoke();
        }
        
        public void Play()
        {
            TimeSpeed = _timeToPause;
            
            IsPause = false;
            OnPlay?.Invoke();
        }
        
        public void SetTimeSpeed(float timeSpeed)
        {
            TimeSpeed = timeSpeed;
            _timeToPause = timeSpeed;
        }
    }
}