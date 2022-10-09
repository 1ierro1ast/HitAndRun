using System;
using Codebase.Core.Scores;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        private IScoreCounter _scoreCounter;

        private void Awake()
        {
            _scoreCounter = GetComponentInParent<IScoreCounter>();
            _scoreCounter.ScoreUpdated += ScoreCounter_OnScoreUpdated;
        }

        private void OnDestroy()
        {
            _scoreCounter.ScoreUpdated -= ScoreCounter_OnScoreUpdated;

        }

        private void ScoreCounter_OnScoreUpdated(int scoreAmount)
        {
            SetScore(scoreAmount);
        }

        private void SetScore(int score)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                if (i < score)
                    _images[i].gameObject.SetActive(true);
                else
                    _images[i].gameObject.SetActive(false);
            }
        }
    }
}