using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.NameSystem;
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
        private INameService _nameService;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _nameService = AllServices.Container.Single<INameService>();
            OpenPopup();
            _createRoomButton?.onClick.AddListener(OnCreateRoomButtonClick);
            _connectToRoomButton?.onClick.AddListener(OnConnectToRoomButtonClick);
            _serverAddress?.onValueChanged.AddListener(OnServerAddressValueChanged);
            _nameField?.onValueChanged.AddListener(OnNameValueChanged);
            
            _nameField.text = _nameService.PlayerName;
            _nameField.characterLimit = _nameService.MaxPlayerNameLength;
        }
        
        private void OnNameValueChanged(string name)
        {
            _nameService.SetPlayerName(name);
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