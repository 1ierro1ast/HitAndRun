namespace Codebase
{
    public interface ICameraTarget
    {
        bool IsLocalPlayer { get; }
        bool CanMove { get; }
        float RotationX { get; }
    }
}