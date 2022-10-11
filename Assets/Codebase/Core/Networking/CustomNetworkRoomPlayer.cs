using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
        [SyncVar(hook = nameof(SyncName))] 
        private string _syncName;
        
        private IUiFactory _uiFactory;
        private INameService _nameService;
        private RoomPopup _roomPopup;
        private CustomNetworkRoomManager _room;
        
        private void Awake()
        {
            _room = NetworkManager.singleton as CustomNetworkRoomManager;
            _nameService = AllServices.Container.Single<INameService>();
            _uiFactory = AllServices.Container.Single<IUiFactory>();

            _roomPopup = _uiFactory.CreateRoomPopup();
            _roomPopup.ChangeLocalReadyStatus += RoomPopup_OnChangeLocalReadyStatus;
        }

        private void OnDestroy()
        {
            _roomPopup.ChangeLocalReadyStatus -= RoomPopup_OnChangeLocalReadyStatus;
        }

        public override void OnStartClient()
        {
            SetRoomPlayerName();
            UpdateViews();
        }

        public override void OnStopClient()
        {
            UpdateViews();
        }

        private void RoomPopup_OnChangeLocalReadyStatus(bool readyStatus)
        {
            CmdChangeReadyState(readyStatus);
        }

        private void SetRoomPlayerName()
        {
            if (!hasAuthority) return;

            _nameService = AllServices.Container.Single<INameService>();
            if (isServer)
            {
                ChangeNameValue(_nameService.PlayerName);
            }
            else
            {
                CmdChangeName(_nameService.PlayerName);
            }
        }

        private void UpdateViews()
        {
            _roomPopup.RoomNetworkUi.UpdateViews(_room.roomSlots);
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            UpdateViews();
        }

        private void SyncName(string oldValue, string newValue)
        {
            _roomPopup.RoomNetworkUi.AddPlayer(_syncName, readyToBegin);
            UpdateViews();
        }

        [Server]
        private void ChangeNameValue(string newValue) => _syncName = newValue;


        [Command]
        private void CmdChangeName(string newValue) => ChangeNameValue(newValue);
    }
}