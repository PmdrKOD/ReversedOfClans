using ReversedOfClans.Utils;
using ReversedOfClans.Logic;
using System.IO;
using ReversedOfClans.Packets.Factory;

namespace ReversedOfClans.Packets.Messages.Server
{
    public class OwnHomeData : PiranhaMessage
    {
        private readonly Player player;

        public OwnHomeData(Messaging client, Player player)
        {
            this.Messaging = client;
            this.player = player;

        }

        public override void Encode()
        {
            base.Encode();
            WriteInt(0);

            WriteInt(0);
            WriteInt(0);
            WriteInt(1);
            WriteString(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..", "ReversedOfClans.Json", "BaseData.json")));
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);



            writeLong(0,1);
            writeLong(0,1);

            

            WriteByte(1);
            WriteInt(0);
            WriteInt(1);
            WriteString("Originals Pomidors");
            WriteInt(13000001);
            WriteInt(0);
            WriteByte(1);
            WriteInt(0);
            WriteInt(1);
            WriteByte(1);
            WriteInt(0);
            WriteInt(1);
            WriteInt(0);
            WriteInt(0);
            WriteInt(10);
            WriteInt(0);
            WriteInt(0);
            
            WriteString("pmdrkdv");
            WriteString(null);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteByte(0);
            WriteByte(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            WriteInt(0);
            Console.WriteLine("[D] Message OwnHomeData has been sent.");
        }

        public override int GetMessageType()
        {
            return 24101;
        }

    }
}
