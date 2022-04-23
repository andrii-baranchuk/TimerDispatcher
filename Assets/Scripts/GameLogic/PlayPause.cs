namespace GameLogic
{
    using Attributes;
    using UnityEngine;

    public class PlayPause : MonoBehaviour
    {
        [EditorButton]
        public void Play()
        {
            Time.timeScale = 1;
        }

        [EditorButton]
        public void Pause()
        {
            Time.timeScale = 0;
        }
    }
}