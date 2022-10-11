using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public struct MatchRestart : NetworkMessage
    {
        public Vector3 Position;
    }
}