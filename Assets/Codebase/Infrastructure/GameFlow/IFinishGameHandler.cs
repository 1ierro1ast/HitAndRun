using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IFinishGameHandler : IService
    {
        bool CheckScore(int score);
        void HandleScore(int score, string name);
    }
}