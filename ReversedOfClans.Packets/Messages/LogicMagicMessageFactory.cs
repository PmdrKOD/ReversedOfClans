using ReversedOfClans.Logic;
using ReversedOfClans.Packets.Messages.Client;

namespace ReversedOfClans.Packets.Factory
{
    public static class LogicMagicMessageFactory
    {
        private static readonly Dictionary<int, Func<byte[], Messaging, PiranhaMessage>> AVAILABLE_PACKETS = new Dictionary<int, Func<byte[], Messaging, PiranhaMessage>>
        {
            { 10101, (data, Messaging) => new Login(Messaging) },
            { 10108, (data, Messaging) => new KeepAlive( Messaging) }
        };

        public static Func<byte[], Messaging, PiranhaMessage>? GetFactory(int packetID)
        {
            return AVAILABLE_PACKETS.TryGetValue(packetID, out var factory) ? factory : null;
        }
    }
}
