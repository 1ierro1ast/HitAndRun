using UnityEngine;

namespace Codebase.Core.Character
{
    public class TagTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITagable tagable))
            {
                tagable.Tag();
            }
        }
    }
}