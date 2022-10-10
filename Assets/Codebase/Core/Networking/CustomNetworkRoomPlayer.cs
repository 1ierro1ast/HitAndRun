using System;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
        private IUiFactory _uiFactory;
        private INameService _nameService;
        private RoomPopup _roomPopup;

        private void Awake()
        {
            _uiFactory = AllServices.Container.Single<IUiFactory>();
            _nameService = AllServices.Container.Single<INameService>();
            _roomPopup = _uiFactory.CreateRoomPopup();
            _roomPopup.ChangeLocalReadyStatus += RoomPopup_OnChangeLocalReadyStatus;
        }

        private void OnDestroy()
        {
            _roomPopup.ChangeLocalReadyStatus -= RoomPopup_OnChangeLocalReadyStatus;
        }

        private void RoomPopup_OnChangeLocalReadyStatus(bool isReady)
        {
            Debug.Log($"ready: {isReady}");
            readyToBegin = isReady;
            CmdChangeReadyState(isReady);
        }

        public override void OnClientEnterRoom()
        {
            base.OnClientEnterRoom();
            _roomPopup.RoomNetwork.AddPlayer(_nameService.PlayerName, readyToBegin);
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            Debug.Log(nameof(ReadyStateChanged));
            base.ReadyStateChanged(oldReadyState, newReadyState);
            _roomPopup.RoomNetwork.UpdateViews();
        }
    }
}