using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public struct PositionMessage : NetworkMessage
    {
        public Vector3 SpawnPosition;
    }
}