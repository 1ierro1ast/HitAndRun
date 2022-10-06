using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IScoreCounter _scoreCounter;

        private void Awake()
        {
            SetText(0);
            _scoreCounter = AllServices.Container.Single<IScoreCounter>();
            _scoreCounter.ScoreUpdated += ScoreCounter_OnScoreUpdated;
        }

        private void OnDestroy()
        {
            _scoreCounter.ScoreUpdated -= ScoreCounter_OnScoreUpdated;
        }

        private void ScoreCounter_OnScoreUpdated(int score)
        {
            SetText(score);
        }

        private void SetText(int score)
        {
            _text.text = score.ToString();
        }
    }
}