using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Codebase.Core.UI
{
    public class RoomNetworkUi : NetworkBehaviour
    {
        [SerializeField] private PlayerUi _playerUiPrefab;
        [SerializeField] private Transform _playersParent;
        [SerializeField] private List<PlayerUi> _playerViews = new List<PlayerUi>(5);

        public void AddPlayer(string playerName, bool readyToBegin)
        {
            PlayerUi playerUi = Instantiate(_playerUiPrefab, _playersParent);
            playerUi.Construct(playerName, readyToBegin);
            _playerViews.Add(playerUi);
        }

        public void UpdateViews(List<NetworkRoomPlayer> networkRoomPlayers)
        {
            for (int i = 0; i < _playerViews.Count; i++)
            {
                if(i+1 > networkRoomPlayers.Count) continue;
                _playerViews[i].SetReadyStatus(networkRoomPlayers[i].readyToBegin);
            }
        }
    }
}