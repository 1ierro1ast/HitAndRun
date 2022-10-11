using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.NameSystem;
using Mirror;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
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

        private void RoomPopup_OnChangeLocalReadyStatus(bool readyStatus)
        {
            CmdChangeReadyState(readyStatus);
        }

        public override void OnStartClient()
        {
            _roomPopup.RoomNetworkUi.AddPlayer(_nameService.PlayerName, readyToBegin);

            UpdateViews();
        }

        public override void OnStopClient()
        {
            UpdateViews();
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            UpdateViews();
        }

        private void UpdateViews()
        {
            _roomPopup.RoomNetworkUi.UpdateViews(_room.roomSlots);
        }
    }
}