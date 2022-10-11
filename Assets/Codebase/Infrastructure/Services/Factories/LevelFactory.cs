using Codebase.Core;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Spawn;
using UnityEngine;

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
            if (_currentLevel != null) Object.Destroy(_currentLevel.gameObject);
            _currentLevel = _assetProvider.Instantiate<Level>(AssetPath.LevelPrefabPath);
            Object.DontDestroyOnLoad(_currentLevel.gameObject);
            _spawnPointsStorage.SetSpawnPoints(_currentLevel.SpawnPoints);
            return _currentLevel;
        }
    }
}