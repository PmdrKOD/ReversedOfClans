using ReversedOfClans.Utils;
using ReversedOfClans.Packets.Factory;
using ReversedOfClans.Packets.Messages.Server;
using ReversedOfClans.Logic;

namespace ReversedOfClans.Packets.Messages.Client
{
    public class Login :  PiranhaMessage
    {
        private readonly Player player;

        public Login(Messaging Messaging)
        {
            this.Messaging = Messaging;
            this.player = new Player(Messaging);
        }

        public override void Decode()
        {
      
        }

        public override void Process()
        {
            var loginOk = new LoginOk(Messaging, player);
            loginOk.Send();
            var ownHomeData = new OwnHomeData(Messaging, player);
            ownHomeData.Send();
        }
    }
}
