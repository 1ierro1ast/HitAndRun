using Codebase.Core.Scores;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class TagTrigger : MonoBehaviour
    {
        private ITagable _tagable;
        private ScoreCounter _scoreCounter;

        private void Awake()
        {
            _tagable = GetComponent<ITagable>();
            _scoreCounter = GetComponent<ScoreCounter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_scoreCounter.hasAuthority) return;
            if (!_scoreCounter.isLocalPlayer) return;
            if (!_tagable.CanTag) return;
            if (other.TryGetComponent(out ITagable tagable))
            {
                tagable.Tag(_scoreCounter.AddScore);
            }
        }
    }
}