using System;

namespace Codebase.Core.Scores
{
    public interface IScoreCounter
    {
        event Action<int> ScoreUpdated;
    }
}