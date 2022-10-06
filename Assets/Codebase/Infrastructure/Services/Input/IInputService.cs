using System;

namespace Codebase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        float VerticalSpeed { get; }
        float HorizontalSpeed { get; }
        float MouseY { get; }
        float MouseX { get; }
        bool JumpButton { get; }
        bool IsRunning { get; }
        event Action FireButtonEvent;
    }
}