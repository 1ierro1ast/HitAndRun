using System;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class RoomPopup : Popup
    {
        [SerializeField] private Button _playerReadyButton;
        [SerializeField] private Button _playerCancelReadyButton;

        private RoomNetworkUi _roomNetworkUi;

        public RoomNetworkUi RoomNetworkUi => _roomNetworkUi;
        public event Action<bool> ChangeLocalReadyStatus;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _roomNetworkUi = GetComponent<RoomNetworkUi>();

            BindListeners();
        }

        private void BindListeners()
        {
            _playerReadyButton?.onClick.AddListener(OnReadyButtonClick);
            _playerCancelReadyButton?.onClick.AddListener(OnCancelReadyButtonClick);
        }

        private void OnCancelReadyButtonClick()
        {
            ChangeLocalReadyStatus?.Invoke(false);
            SetReadyStatusButton(false);
        }

        private void OnReadyButtonClick()
        {
            ChangeLocalReadyStatus?.Invoke(true);
            SetReadyStatusButton(true);
        }

        private void SetReadyStatusButton(bool isReady)
        {
            _playerReadyButton.gameObject.SetActive(!isReady);
            _playerCancelReadyButton.gameObject.SetActive(isReady);
        }
    }
}