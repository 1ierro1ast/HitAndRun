using TMPro;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class LoadingCurtain : Popup
    {
        [SerializeField] private TMP_Text _winnerView;
        
        private bool _showWinnerView;

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _winnerView.gameObject.SetActive(_showWinnerView);
        }

        protected override void OnClosePopup()
        {
            base.OnClosePopup();
            _showWinnerView = false;
        }

        public void SetWinnerView(string winnerName)
        {
            _showWinnerView = true;
            _winnerView.text = winnerName;
        }
    }
}