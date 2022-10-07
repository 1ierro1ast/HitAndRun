using UnityEngine;

namespace Codebase.Core.Character
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class TagTrigger : MonoBehaviour
    {
        private ITagable _tagable;
        private NetworkCharacter _networkCharacter;

        private void Awake()
        {
            _tagable = GetComponent<ITagable>();
            _networkCharacter = GetComponent<NetworkCharacter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_networkCharacter.hasAuthority) return;
            if (!_networkCharacter.isLocalPlayer) return;
            if (!_tagable.CanTag) return;
            if (other.TryGetComponent(out ITagable tagable))
            {
                _networkCharacter.AddScore();
                tagable.Tag();
            }
        }
    }
}