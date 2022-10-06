using Codebase.Core.Settings;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.NameSystem;
using Codebase.Infrastructure.Services.Score;
using Mirror;

namespace Codebase.Infrastructure.GameFlow
{
    public class FinishGameHandler : IFinishGameHandler
    {
        private readonly INameService _nameService;
        private readonly GameplaySettings _gameplaySettings;

        public FinishGameHandler(IScoreCounter scoreCounter, IAssetProvider assetProvider, INameService nameService)
        {
            _nameService = nameService;

            scoreCounter.ScoreUpdated += ScoreCounter_OnScoreUpdated;

            _gameplaySettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath)
                .GameplaySettings;
        }

        private void ScoreCounter_OnScoreUpdated(int score)
        {
            if (score >= _gameplaySettings.GoalScore)
                NetworkServer.SendToAll(new MatchEnd {PlayerName = _nameService.PlayerName});
        }
    }
}