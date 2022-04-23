namespace Infrastructure.Services.Timer
{
    using System;
    using UnityEngine;

    public class Timer
    {
        public float Duration { get; }
        
        public bool Paused { get; private set; }
        public bool Canceled { get; private set; }
        public bool Completed { get; private set; }
        public bool Done => Completed || Canceled || OwnerDestroyed;
        
        public float TimeLeft { get; private set; }
        public float TimeElapsed { get; private set; }
        
        public float RatioRemaining => TimeLeft / Duration;
        public float RatioComplete => TimeElapsed / Duration;

        private bool OwnerDestroyed => _haveOwner && _owner == null;

        private readonly bool _looped;
        private readonly bool _ignoreTimeScale;

        private readonly Action _onComplete;
        private readonly Action<float> _onUpdate;

        private readonly MonoBehaviour _owner;
        private readonly bool _haveOwner;
        
        private readonly ITimerDispatcher _timerDispatcher;

        public Timer
        (
            float duration, 
            Action onComplete,
            Action<float> onUpdate = null,
            bool looped = false,
            bool ignoreTimeScale = false,
            MonoBehaviour owner = null
        )
        {
            Duration = duration;
            _onComplete = onComplete;
            _onUpdate = onUpdate;
            _looped = looped;
            _ignoreTimeScale = ignoreTimeScale;
            _owner = owner;
            _haveOwner = _owner != null;
            
            _timerDispatcher = ServiceLocator.Container.Single<ITimerDispatcher>();
            
            Start();
        }
        
        private void Start()
        {
            TimeLeft = Duration;
            TimeElapsed = 0;
            Completed = false;
            Canceled = false;
            
            if(_timerDispatcher == null)
                Debug.LogError("TimerDispatcher not registered");
            else
                _timerDispatcher.Register(this);
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public void Cancel()
        {
            Canceled = true;
        }

        public void Update()
        {
            if (Paused || Done) return;

            TimeLeft -= DeltaTime();
            TimeElapsed += DeltaTime();
            _onUpdate?.Invoke(TimeLeft);

            if (TimeLeft <= 0)
            {
                _onComplete?.Invoke();

                if (_looped)
                {
                    TimeLeft = Duration;
                    TimeElapsed = 0;
                }
                else
                {
                    Completed = true;
                }
            }
        }

        private float DeltaTime()
        {
            return _ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        }
    }
}