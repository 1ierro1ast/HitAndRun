using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : Popup
    {
        public event Action StartButtonClickEvent;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _connectToRoomButton;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private TMP_InputField _serverAddress;
        
        private NetworkManager _networkManager;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            _createRoomButton?.onClick.AddListener(OnCreateRoomButtonClick);
            _connectToRoomButton?.onClick.AddListener(OnConnectToRoomButtonClick);
            _serverAddress?.onValueChanged.AddListener(OnServerAddressValueChanged);
            _nameField?.onValueChanged.AddListener(OnNameValueChanged);
        }
        
        private void OnNameValueChanged(string name)
        {
            
        }

        private void OnServerAddressValueChanged(string address)
        {
            _networkManager.networkAddress = address;
        }

        private void OnConnectToRoomButtonClick()
        {
            if (NetworkClient.isConnected && NetworkServer.active) return;
            _networkManager.StartClient();
            StartButtonClickEvent?.Invoke();
        }

        private void OnCreateRoomButtonClick()
        {
            if (NetworkClient.isConnected && NetworkServer.active) return;
            _networkManager.StartHost();
            StartButtonClickEvent?.Invoke();
        }

        public void SetNetworkManager(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }
    }
}