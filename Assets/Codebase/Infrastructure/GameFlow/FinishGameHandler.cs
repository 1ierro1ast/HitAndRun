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
        private readonly IEventBus _eventBus;
        private readonly GameplaySettings _gameplaySettings;

        public FinishGameHandler(IAssetProvider assetProvider, INameService nameService, IEventBus eventBus)
        {
            _nameService = nameService;
            _eventBus = eventBus;

            _gameplaySettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath)
                .GameplaySettings;
        }

        public void RegisterScoreCounter(IScoreCounter scoreCounter)
        {
            scoreCounter.ScoreUpdated += HandleScore;
        }
        
        public void DisposeScoreCounter(IScoreCounter scoreCounter)
        {
            scoreCounter.ScoreUpdated -= HandleScore;
        }

        public void HandleScore(int score)
        {
            if (score >= _gameplaySettings.GoalScore)
            {
                NetworkServer.SendToAll(new MatchEnd{WinnerName = _nameService.PlayerName});
            }
        }
    }
}