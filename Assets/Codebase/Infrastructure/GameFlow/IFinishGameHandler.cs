using Codebase.Core.Scores;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IFinishGameHandler : IService
    {
        void RegisterScoreCounter(IScoreCounter scoreCounter);
        void DisposeScoreCounter(IScoreCounter scoreCounter);
    }
}