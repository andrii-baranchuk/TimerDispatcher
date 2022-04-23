namespace Infrastructure.Services.Timer
{
    public interface ITimerDispatcher : IService
    {
        void Register(Timer timer);
    }
}