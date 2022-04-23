namespace Infrastructure
{
    using Services;
    using Services.Tick;
    using Services.Timer;
    using UnityEngine;

    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private TickService _tickService;
        
        private Game _game;
        private ServiceLocator _serviceLocator;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _serviceLocator = ServiceLocator.Container;
            RegisterServices();
            
            _game = new Game();
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<ITickService>(_tickService);
            _serviceLocator.RegisterSingle<ITimerDispatcher>
                (new TimerDispatcher(_serviceLocator.Single<ITickService>()));
        }
    }
}