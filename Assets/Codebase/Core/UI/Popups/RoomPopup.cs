using System;
using Codebase.Core.Networking;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class RoomPopup : Popup
    {
        [SerializeField] private Button _playerReadyButton;
        [SerializeField] private Button _playerCancelReadyButton;
        private RoomNetwork _roomNetwork;
        private CustomNetworkRoomManager _networkManager;

        public RoomNetwork RoomNetwork => _roomNetwork;
        public event Action<bool> ChangeLocalReadyStatus;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _roomNetwork = GetComponent<RoomNetwork>();
            _playerReadyButton?.onClick.AddListener(OnReadyButtonClick);
            _playerCancelReadyButton?.onClick.AddListener(OnCancelReadyButtonClick);
        }

        private void OnCancelReadyButtonClick()
        {
            Debug.Log("cancel ready");
            ChangeLocalReadyStatus?.Invoke(false);
            SetReadyStatusButton(false);
        }

        private void OnReadyButtonClick()
        {
            Debug.Log("is ready");
            ChangeLocalReadyStatus?.Invoke(true);
            SetReadyStatusButton(true);
        }

        private void SetReadyStatusButton(bool isReady)
        {
            _playerReadyButton.gameObject.SetActive(isReady);
            _playerCancelReadyButton.gameObject.SetActive(!isReady);
        }

        public void SetNetworkManager(CustomNetworkRoomManager networkManager)
        {
            _networkManager = networkManager;
            _networkManager.AllPlayersReady += NetworkManager_OnAllPlayersReady;
        }

        private void OnDestroy()
        {
            _networkManager.AllPlayersReady -= NetworkManager_OnAllPlayersReady;
        }

        private void NetworkManager_OnAllPlayersReady()
        {
            
        }
    }
}