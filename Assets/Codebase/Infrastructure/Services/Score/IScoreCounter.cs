using System;

namespace Codebase.Infrastructure.Services.Score
{
    public interface IScoreCounter : IService
    {
        event Action<int> ScoreUpdated; 
        void AddScore(int scoreAmount = 1);
        void CleanScore();
        int Score { get; }
    }
}