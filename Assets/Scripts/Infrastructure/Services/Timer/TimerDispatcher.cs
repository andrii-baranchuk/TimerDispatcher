namespace Infrastructure.Services.Timer
{
    using System;
    using System.Collections.Generic;
    using Tick;

    public class TimerDispatcher : ITimerDispatcher, IDisposable
    {
        private readonly ITickService _tickService;

        private readonly List<Timer> _timers = new List<Timer>();
        private readonly List<Timer> _timersToRegister = new List<Timer>();

        public TimerDispatcher(ITickService tickService)
        {
            _tickService = tickService;
            _tickService.Tick += OnTick;
        }

        public void Register(Timer timer)
        {
            _timersToRegister.Add(timer);
        }

        private void OnTick()
        {
            if (_timersToRegister.Count > 0)
            {
                _timers.AddRange(_timersToRegister);
                _timersToRegister.Clear();
            }
            
            foreach (var timer in _timers)
            {
                timer.Update();
            }

            _timers.RemoveAll(t => t.Done);
        }

        public void Dispose()
        {
            _tickService.Tick -= OnTick;
        }
    }
}