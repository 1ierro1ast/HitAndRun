using Codebase.Core.Scores;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class TagTrigger : NetworkBehaviour
    {
        private ICanBeingTagged _canBeingTagged;
        private ScoreCounter _scoreCounter;

        private void Awake()
        {
            _canBeingTagged = GetComponent<ICanBeingTagged>();
            _scoreCounter = GetComponent<ScoreCounter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canBeingTagged.CanTag) return;
            if (other.TryGetComponent(out ICanBeingTagged canBeingTagged))
            {
                canBeingTagged.Tag(_scoreCounter.AddScore);
            }
        }
    }
}