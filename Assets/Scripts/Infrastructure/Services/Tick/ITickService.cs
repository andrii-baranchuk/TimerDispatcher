namespace Infrastructure.Services.Tick
{
    using System;
    
    public interface ITickService : IService
    {
        event Action Tick;
    }
}