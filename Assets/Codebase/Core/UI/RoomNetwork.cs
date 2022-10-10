using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class RoomNetwork : NetworkBehaviour
    {
        [SerializeField] private PlayerUi _playerUiPrefab;
        [SerializeField] private Transform _playersParent;
        private readonly SyncList<PlayerItem> _players = new SyncList<PlayerItem>();
        private List<PlayerUi> _playerViews = new List<PlayerUi>(5);
        
        public void AddPlayer(string playerName, bool readyToBegin)
        {
            PlayerItem item = new PlayerItem
            {
                PlayerName = playerName,
                IsPlayerReady = readyToBegin
            };
            PlayerUi playerUi = Instantiate(_playerUiPrefab, _playersParent);
            playerUi.Construct(playerName, readyToBegin);
            _players.Add(item);
        }

        public void UpdateViews()
        {
            for (int i = 0; i < _playerViews.Count; i++)
            {
                Debug.Log(i);
                _playerViews[i].SetReadyStatus(_players[i].IsPlayerReady);
            }
        }
    }

    public struct PlayerItem
    {
        public string PlayerName;
        public bool IsPlayerReady;
    }
}