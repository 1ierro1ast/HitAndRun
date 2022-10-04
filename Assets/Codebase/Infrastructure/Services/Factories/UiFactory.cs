using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public class UiFactory : IUiFactory
    {
        private IAssetProvider _assetProvider;

        private StartPopup _startPopup;
        private OverlayPopup _overlayPopup;

        public UiFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            InitializePopups();
        }

        private void InitializePopups()
        {
            CreateStartPopup();
            CreateOverlayPopup();
        }
        public StartPopup CreateStartPopup()
        {
            //Debug.Log("start popup");
            if (_startPopup == null)
            {
                _startPopup = _assetProvider.Instantiate<StartPopup>(AssetPath.StartPopupPath);
                Object.DontDestroyOnLoad(_startPopup);
            }
            return _startPopup;
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