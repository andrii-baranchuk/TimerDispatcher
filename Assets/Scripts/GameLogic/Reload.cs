namespace GameLogic
{
    using Attributes;
    using Infrastructure.Services.Timer;
    using UnityEngine;

    public class Reload : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private bool _looped;
        [SerializeField] private bool _ignoreTimeScale;
        
        private Timer _reloadTimer;

        [EditorButton]
        public void StartReload()
        {
            _reloadTimer = new Timer(_duration, Reloaded, Reloading, _looped, _ignoreTimeScale, this);
        }
        
        [EditorButton]
        public void PauseReload()
        {
            _reloadTimer?.Pause();
        }

        [EditorButton]
        public void ResumeReload()
        {
            _reloadTimer?.Resume();
        }

        [EditorButton]
        public void CancelReload()
        {
            _reloadTimer?.Cancel();
        }

        [EditorButton]
        public void RestartReload()
        {
            _reloadTimer?.Cancel();
            StartReload();
        }

        private void Reloading(float timeLeft)
        {
            Debug.Log($"Reloaded in {_reloadTimer.RatioComplete}");
        }

        private void Reloaded()
        {
            Debug.Log("Reloaded");
        }
    }
}