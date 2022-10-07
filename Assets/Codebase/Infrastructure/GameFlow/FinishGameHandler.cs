using Codebase.Core.Settings;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.NameSystem;
using UnityEngine;

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

        private void ScoreCounter_OnScoreUpdated(int score)
        {
            if (score >= _gameplaySettings.GoalScore)
            {
                Debug.Log(000);
            }
        }
    }
}