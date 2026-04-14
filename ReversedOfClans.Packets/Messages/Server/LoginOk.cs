using ReversedOfClans.Utils;
using ReversedOfClans.Logic;
using ReversedOfClans.Packets.Factory;

namespace ReversedOfClans.Packets.Messages.Server
{
    public class LoginOk : PiranhaMessage
    {
        private readonly Player player;

        public LoginOk(Messaging Messaging, Player player)
        {
            this.Messaging = Messaging;
            this.player = player;

            this.version = 1;
        }

        public override void Encode()
        {
            WriteInt(0);
            WriteInt(1);
            WriteInt(0);
            WriteInt(1);
            WriteString("a77bad4dc5241ccb44d5a541376396208f92af8");
            WriteString(null);
            WriteString(null);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteString("dev");
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteString(null);
            Console.WriteLine("[D] Message LoginOk has been sent.");
        }
        
        public override int GetMessageType()
        {
            return 20104;
        }

        
    }
}
