using System;
using Codebase.Core.Character;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Scores
{
    public class ScoreCounter : NetworkBehaviour, IScoreCounter
    {
        [SyncVar(hook = nameof(SyncScores))] 
        private int _syncScores;

        private IFinishGameHandler _finishGameHandler;
        private INameHolder _nameHolder;
        private int _scores;

        public event Action<int> ScoreUpdated;

        private void Start()
        {
            _nameHolder = GetComponent<INameHolder>();
            ScoreUpdated?.Invoke(_scores);

            _finishGameHandler = AllServices.Container.Single<IFinishGameHandler>();
        }

        public void AddScore()
        {
            _scores++;
            SetScores(_scores);
            
            ScoreUpdated?.Invoke(_scores);
        }

        public void CleanScores()
        {
            _scores = 0;
            SetScores(_scores);
            
            ScoreUpdated?.Invoke(_scores);
        }

        private void SetScores(int scores)
        {
            if (isServer)
            {
                ChangeScoresValue(scores);
            }
            else
            {
                CmdChangeScores(scores);
            }
        }

        private void SyncScores(int oldValue, int newValue)
        {
            _scores = newValue;
            ScoreUpdated?.Invoke(_scores);
            _finishGameHandler.HandleScore(_scores, _nameHolder.Name);
        }

        [Server]
        private void ChangeScoresValue(int newValue) => _syncScores = newValue;

        [Command]
        private void CmdChangeScores(int newValue) => ChangeScoresValue(newValue);
    }
}