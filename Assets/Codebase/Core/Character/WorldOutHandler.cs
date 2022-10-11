using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Spawn;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class WorldOutHandler : MonoBehaviour
    {
        [SerializeField] private MoveController _moveController;
        private ISpawnPointsStorage _spawnPointsStorage;

        private void Awake()
        {
            _spawnPointsStorage = AllServices.Container.Single<ISpawnPointsStorage>();
        }

        private void Update()
        {
            if (transform.position.y < -200)
                _moveController.MoveToSpawnPoint(_spawnPointsStorage.GetSpawnPoint().position);
        }
    }
}