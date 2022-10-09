using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Scores
{
    public class ScoreCounter : NetworkBehaviour, IScoreCounter
    {
        [SyncVar(hook = nameof(SyncScores))] private int _syncScores;
        private int _scores;
        private IFinishGameHandler _finishGameHandler;
        public event Action<int> ScoreUpdated;

        private void Start()
        {
            ScoreUpdated?.Invoke(_scores);
            
            if (!isLocalPlayer) return;
            _finishGameHandler = AllServices.Container.Single<IFinishGameHandler>();
            _finishGameHandler.RegisterScoreCounter(this);
        }

        private void OnDestroy()
        {
            if (!isLocalPlayer) return;
            _finishGameHandler.DisposeScoreCounter(this);
        }

        private void SyncScores(int oldValue, int newValue)
        {
            _scores = newValue;
        }

        [Server]
        private void ChangeScoresValue(int newValue)
        {
            _syncScores = newValue;
        }

        [Command]
        private void CmdChangeScores(int newValue)
        {
            ChangeScoresValue(newValue);
        }

        public void AddScore()
        {
            _scores++;
            if (isServer)
            {
                ChangeScoresValue(_scores);
            }
            else
            {
                CmdChangeScores(_scores);
            }
            ScoreUpdated?.Invoke(_scores);
        }


        private void Update()
        {
            if (hasAuthority)
            {
                if(Input.GetKeyDown(KeyCode.J)) AddScore();
            }
        }
    }
}