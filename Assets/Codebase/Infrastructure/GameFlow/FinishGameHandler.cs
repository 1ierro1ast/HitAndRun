using Codebase.Core.Networking;
using Codebase.Core.Scores;
using Codebase.Core.Settings;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;

namespace Codebase.Infrastructure.GameFlow
{
    public class FinishGameHandler : IFinishGameHandler
    {
        private readonly INameService _nameService;
        private readonly GameplaySettings _gameplaySettings;

        public FinishGameHandler(IAssetProvider assetProvider, INameService nameService)
        {
            _nameService = nameService;

            _gameplaySettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath)
                .GameplaySettings;
        }

        public void RegisterScoreCounter(IScoreCounter scoreCounter)
        {
            scoreCounter.ScoreUpdated += ScoreCounter_OnScoreUpdated;
        }
        
        public void DisposeScoreCounter(IScoreCounter scoreCounter)
        {
            scoreCounter.ScoreUpdated -= ScoreCounter_OnScoreUpdated;
        }

        private void ScoreCounter_OnScoreUpdated(int score)
        {
            if (score >= _gameplaySettings.GoalScore)
            {
                NetworkServer.SendToAll(new MatchEnd{WinnerName = _nameService.PlayerName});
            }
        }
    }
}