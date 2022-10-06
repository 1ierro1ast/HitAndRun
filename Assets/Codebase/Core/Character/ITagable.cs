namespace Codebase.Core.Character
{
    public interface ITagable
    {
        bool CanTag { get; }
        void Tag();
    }
}