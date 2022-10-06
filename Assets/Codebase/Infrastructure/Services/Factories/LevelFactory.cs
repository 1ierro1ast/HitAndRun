using Codebase.Core;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Spawn;

namespace Codebase.Infrastructure.Services.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISpawnPointsStorage _spawnPointsStorage;
        private Level _currentLevel;

        public LevelFactory(IAssetProvider assetProvider, ISpawnPointsStorage spawnPointsStorage)
        {
            _assetProvider = assetProvider;
            _spawnPointsStorage = spawnPointsStorage;
        }
        public Level GetLevel()
        {
            if (_currentLevel != null) UnityEngine.Object.Destroy(_currentLevel);
            _currentLevel = _assetProvider.Instantiate<Level>(AssetPath.LevelPrefabPath);
            _spawnPointsStorage.SetSpawnPoints(_currentLevel.SpawnPoints);
            return _currentLevel;
        }
    }
}