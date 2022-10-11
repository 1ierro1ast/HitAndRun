using UnityEngine;

namespace Codebase.Infrastructure.Services.Spawn
{
    public class SpawnPointsStorage : ISpawnPointsStorage
    {
        private Transform[] _spawnPoints;

        public void SetSpawnPoints(Transform[] currentLevelSpawnPoints) => _spawnPoints = currentLevelSpawnPoints;

        public Transform GetSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}