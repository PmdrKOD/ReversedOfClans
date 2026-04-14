using ReversedOfClans.Utils;
using ReversedOfClans.Logic;
using ReversedOfClans.Packets.Factory;

namespace ReversedOfClans.Packets.Messages.Server
{
    public class KeepAliveOk : PiranhaMessage
    {
        public KeepAliveOk(Messaging Messaging)
        {
            this.Messaging = Messaging;
        }

        public override int GetMessageType()
        {
            return 20108;
        }
    }
}
