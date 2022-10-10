namespace Codebase.Core.Character.Cameras
{
    public interface ICameraTarget
    {
        bool IsLocalPlayer { get; }
        bool CanMove { get; }
        float RotationX { get; }
    }
}