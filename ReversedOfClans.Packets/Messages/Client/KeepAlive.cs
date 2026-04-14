using ReversedOfClans.Utils;
using ReversedOfClans.Packets.Factory;
using ReversedOfClans.Packets.Messages.Server;
using ReversedOfClans.Logic;

namespace ReversedOfClans.Packets.Messages.Client
{
    public class KeepAlive : PiranhaMessage
    {

        public KeepAlive(Messaging Messaging)
        {
            this.Messaging = Messaging;
        }

        public override void Decode()
        {
            // Decode logic can be added here
        }

        public override void Process()
        {
            var keepAliveOk = new KeepAliveOk(Messaging);
            keepAliveOk.Send();
        }
    }
}
