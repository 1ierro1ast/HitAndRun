using System;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomPlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(HandleDisplayNameChanged))]
        public string DisplayName = "Loading...";

        [SyncVar(hook = nameof(HandleReadyStatusChanged))]
        public bool IsReady = false;
        
        private bool _isLeader;
        private IUiFactory _uiFactory;
        private INameService _nameService;
        private IEventBus _eventBus;
        private RoomPopup _roomPopup;
        private CustomNetworkRoomManager _room;

        public event Action ShowStartButton;


        public bool IsLeader
        {
            set => _isLeader = value;
        }

        private void Awake()
        {
            _room = NetworkManager.singleton as CustomNetworkRoomManager;
            _nameService = AllServices.Container.Single<INameService>();
            _eventBus = AllServices.Container.Single<IEventBus>();
            _uiFactory = AllServices.Container.Single<IUiFactory>();

            _roomPopup = _uiFactory.CreateRoomPopup();

            _roomPopup.ChangeLocalReadyStatus += RoomPopup_OnChangeLocalReadyStatus;
        }

        private void OnDestroy()
        {
            _roomPopup.ChangeLocalReadyStatus -= RoomPopup_OnChangeLocalReadyStatus;
        }

        private void RoomPopup_OnChangeLocalReadyStatus(bool readyStatus)
        {
            CmdReadyUp(readyStatus);
        }

        public override void OnStartAuthority()
        {
            CmdSetDisplayName(_nameService.PlayerName);
        }

        public override void OnStartClient()
        {
            Debug.Log("Start client");
            _room.RoomPlayers.Add(this);
            _roomPopup.RoomNetworkUi.AddPlayer(_nameService.PlayerName, IsReady);

            UpdateDisplay();
        }

        public override void OnStopClient()
        {
            _room.RoomPlayers.Remove(this);

            UpdateDisplay();
        }

        public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
        public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

        private void UpdateDisplay()
        {
            _roomPopup.RoomNetworkUi.UpdateViews(_room.RoomPlayers);
        }

        [Command]
        private void CmdSetDisplayName(string displayName)
        {
            DisplayName = displayName;
        }

        [Command]
        private void CmdReadyUp(bool readyStatus)
        {
            IsReady = readyStatus;

            _room.NotifyPlayersOfReadyState();
        }
    }
}