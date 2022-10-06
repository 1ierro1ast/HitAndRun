using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Score;
using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class TagTrigger : MonoBehaviour
    {
        private ITagable _tagable;
        private IScoreCounter _scoreCounter;

        private void Awake()
        {
            _tagable = GetComponent<ITagable>();
            _scoreCounter = AllServices.Container.Single<IScoreCounter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_tagable.CanTag) return;
            if (other.TryGetComponent(out ITagable tagable))
            {
                _scoreCounter.AddScore();
                tagable.Tag();
            }
        }
    }
}