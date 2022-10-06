using UnityEngine;

namespace Codebase.Core
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        public Transform[] SpawnPoints => _spawnPoints;
    }
}