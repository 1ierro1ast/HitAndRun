using Codebase.Core.Settings;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.GameFlow
{
    public class FinishGameHandler : IFinishGameHandler
    {
        private readonly IEventBus _eventBus;
        private readonly GameplaySettings _gameplaySettings;

        public FinishGameHandler(IAssetProvider assetProvider, IEventBus eventBus)
        {
            _eventBus = eventBus;

            _gameplaySettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath)
                .GameplaySettings;
        }

        public bool CheckScore(int score) => score >= _gameplaySettings.GoalScore;

        public void HandleScore(int score, string name)
        {
            if (CheckScore(score))
                _eventBus.BroadcastLevelFinished(name);
        }
    }
}