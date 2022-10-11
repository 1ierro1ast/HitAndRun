using Codebase.Core.Networking;
using Codebase.Core.Scores;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class MatchRestartHandler : NetworkBehaviour
    {
        [SerializeField] private MoveController _moveController;
        [SerializeField] private ScoreCounter _scoreCounter;
        private void Awake()
        {
            NetworkClient.RegisterHandler<MatchRestart>(OnMatchRestart);
        }

        private void OnMatchRestart(MatchRestart obj)
        {
            _scoreCounter.CleanScores();
            _moveController.MoveToSpawnPoint(obj.Position);
        }
    }
}