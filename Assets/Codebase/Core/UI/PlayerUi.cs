using TMPro;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class PlayerUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _isPlayerReady;

        public void Construct(string playerName, bool isPlayerReady)
        {
            _playerName.text = playerName;
            SetReadyStatus(isPlayerReady);
        }

        public void SetReadyStatus(bool isPlayerReady)
        {
            Debug.Log($"{isPlayerReady}");
            _isPlayerReady.text = isPlayerReady ? "<color=green>READY</color>" : "<color=red>NOT READY</color>";
        }
    }
}