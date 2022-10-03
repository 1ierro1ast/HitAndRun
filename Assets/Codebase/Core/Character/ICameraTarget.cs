namespace Codebase.Core.Character
{
    public interface ICameraTarget
    {
        bool IsLocalPlayer { get; }
        bool CanMove { get; }
        float RotationX { get; }
    }
}