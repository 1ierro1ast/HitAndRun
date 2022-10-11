using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly INetworkFactory _networkFactory;

        private StartPopup _startPopup;
        private RoomPopup _roomPopup;
        private OverlayPopup _overlayPopup;

        public UiFactory(IAssetProvider assetProvider, INetworkFactory networkFactory)
        {
            _assetProvider = assetProvider;
            _networkFactory = networkFactory;
            InitializePopups();
        }

        private void InitializePopups()
        {
            CreateStartPopup();
            CreateRoomPopup();
            CreateOverlayPopup();
        }
        public StartPopup CreateStartPopup()
        {
            if (_startPopup == null)
            {
                _startPopup = _assetProvider.Instantiate<StartPopup>(AssetPath.StartPopupPath);
                _startPopup.SetNetworkManager(_networkFactory.GetNetworkManager());
                Object.DontDestroyOnLoad(_startPopup);
            }
            return _startPopup;
        }

        public RoomPopup CreateRoomPopup()
        {
            if (_roomPopup == null)
            {
                _roomPopup = _assetProvider.Instantiate<RoomPopup>(AssetPath.RoomPopupPath);
                Object.DontDestroyOnLoad(_roomPopup);
            }
            return _roomPopup;
        }

        public OverlayPopup CreateOverlayPopup()
        {
            if (_overlayPopup == null)
            {
                _overlayPopup = _assetProvider.Instantiate<OverlayPopup>(AssetPath.OverlayPopupPath);
                Object.DontDestroyOnLoad(_overlayPopup);
            }
            return _overlayPopup;
        }
    }
}