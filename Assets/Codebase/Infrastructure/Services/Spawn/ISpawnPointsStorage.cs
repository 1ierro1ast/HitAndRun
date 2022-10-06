using UnityEngine;

namespace Codebase.Infrastructure.Services.Spawn
{
    public interface ISpawnPointsStorage : IService
    {
        void SetSpawnPoints(Transform[] currentLevelSpawnPoints);
        Transform GetSpawnPoint();
    }
}