using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : Popup
    {
        public event Action StartButtonClickEvent;
        [SerializeField] private Button _startButton;
        private IEventBus _eventBus;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            _startButton?.onClick.AddListener(OnStartButtonClick);
            _eventBus = AllServices.Container.Single<IEventBus>();
            
            _eventBus.GamePlayStartEvent += GameFlow_BroadcasterOnGamePlayStart;
        }

        private void OnDestroy()
        {
            _eventBus.GamePlayStartEvent -= GameFlow_BroadcasterOnGamePlayStart;
        }

        private void GameFlow_BroadcasterOnGamePlayStart()
        {
            Debug.Log("Game play start");
            ClosePopup();
        }

        private void OnStartButtonClick()
        {
            StartButtonClickEvent?.Invoke();
        }

        public void BroadcastStartLevel()
        {
            StartButtonClickEvent?.Invoke();
        }
    }
}