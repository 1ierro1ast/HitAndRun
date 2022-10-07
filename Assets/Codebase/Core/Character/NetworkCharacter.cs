using Codebase.Core.UI;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class NetworkCharacter : NetworkBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        private int _scores;
        [SyncVar(hook = nameof(SyncScores))] 
        private int _syncScores;
        
        private void Awake()
        {
            _scoreView.SetScore(_scores);
        }

        private void SyncScores(int oldValue, int newValue)
        {
            _scores = newValue;
        }
        
        [Server]
        public void ChangeScoresValue(int newValue)
        {
            _syncScores = newValue;
        }
        [Command]
        public void CmdChangeScores(int newValue)
        {
            ChangeScoresValue(newValue);
        }

        public void AddScore()
        {
            _scores++;
            if (hasAuthority)
            {
                if (isServer)
                {
                    ChangeScoresValue(_scores);
                }
                else
                {
                    CmdChangeScores(_scores);
                }
            }

            _scoreView.SetScore(_scores);
        }
    }
}