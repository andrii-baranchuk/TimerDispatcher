namespace Infrastructure.Services.Tick
{
    using System;
    using UnityEngine;

    [Serializable]
    public class TickService : MonoBehaviour, ITickService
    {
        public event Action Tick;
        
        private void Update()
        {
            Tick?.Invoke();
        }
    }
}