using Mirror;

namespace Codebase.Infrastructure.GameFlow
{
    public struct MatchEnd : NetworkMessage
    {
        public string PlayerName;
    }
}