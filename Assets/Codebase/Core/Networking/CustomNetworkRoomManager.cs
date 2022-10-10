using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomManager : NetworkManager
    {
        [SerializeField] private int _minPlayers = 2;
        [Scene] [SerializeField] private string _lobbyScene = string.Empty;


        [Header("Room")] [SerializeField] private CustomNetworkRoomPlayer _roomPlayerPrefab = null;

        [Header("Game")] [SerializeField] private NetworkBehaviour _gamePlayerPrefab = null;
        [Scene] [SerializeField] private string _gameScene = string.Empty;

        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        public static event Action<NetworkConnection> OnServerReadied;
        public static event Action OnServerStopped;

        [SerializeField]private List<CustomNetworkRoomPlayer> _roomPlayers = new List<CustomNetworkRoomPlayer>();
        private List<NetworkBehaviour> _gamePlayers = new List<NetworkBehaviour>();

        public List<CustomNetworkRoomPlayer> RoomPlayers => _roomPlayers;
        public List<NetworkBehaviour> GamePlayers => _gamePlayers;

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();

            OnClientDisconnected?.Invoke();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                return;
            }

            if (SceneManager.GetActiveScene().name != _lobbyScene)
            {
                conn.Disconnect();
                return;
            }
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            bool isLeader = RoomPlayers.Count == 0;
            CustomNetworkRoomPlayer roomPlayerInstance = Instantiate(_roomPlayerPrefab);
            roomPlayerInstance.IsLeader = isLeader;
            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<CustomNetworkRoomPlayer>();
                RoomPlayers.Remove(player);
                NotifyPlayersOfReadyState();
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            OnServerStopped?.Invoke();

            RoomPlayers.Clear();
            GamePlayers.Clear();
        }

        public void NotifyPlayersOfReadyState()
        {
            if (IsReadyToStart())
            {
                StartGame();
            }
        }

        private bool IsReadyToStart()
        {
            Debug.Log(numPlayers);
            if (numPlayers < _minPlayers)
            {
                return false;
            }

            foreach (var player in RoomPlayers)
            {
                if (!player.IsReady)
                {
                    return false;
                }
            }

            return true;
        }

        public void StartGame()
        {
            Debug.Log("Start game");
            if (SceneManager.GetActiveScene().name == _lobbyScene)
            {
                Debug.Log("Start");
                ServerChangeScene(_gameScene);
            }
        }

        public override void ServerChangeScene(string newSceneName)
        {
            if (SceneManager.GetActiveScene().name == _lobbyScene)
            {
                for (int i = RoomPlayers.Count - 1; i >= 0; i--)
                {
                    var connection = RoomPlayers[i].connectionToClient;
                    var gamePlayerInstance = Instantiate(_gamePlayerPrefab);

                    NetworkServer.Destroy(connection.identity.gameObject);
                    NetworkServer.ReplacePlayerForConnection(connection, gamePlayerInstance.gameObject);
                }
            }

            base.ServerChangeScene(newSceneName);
        }

        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);
            OnServerReadied?.Invoke(conn);
        }
    }
}