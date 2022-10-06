using System;

namespace Codebase.Infrastructure.Services.Score
{
    public class ScoreCounter : IScoreCounter
    {
        private int _score;

        public int Score => _score;
        
        public event Action<int> ScoreUpdated;

        public void CleanScore()
        {
            _score = 0;
            ScoreUpdated?.Invoke(_score);
        }


        public void AddScore(int scoreAmount = 1)
        {
            _score += scoreAmount;
            ScoreUpdated?.Invoke(_score);
        }
    }
}