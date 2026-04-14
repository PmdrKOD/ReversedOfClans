using ReversedOfClans.Logic;
using ReversedOfClans.Utils;

namespace ReversedOfClans.Packets.Factory
{
    public class PiranhaMessage :  ChecksumEncoder
    {

        public int? version;

        public PiranhaMessage(Messaging Messaging = null)
        {
            this.Messaging = Messaging;
        }
       
        public virtual void Decode(){}
        public virtual void Encode(){}
        public virtual void Process(){}
        public virtual int GetMessageType()
        {
            return 0;
        }
        
        public void Send()
        {
            Encode();
            Messaging.SendData(GetMessageType(), buffer.ToArray(), version);
        }
        public virtual bool IsClientToServerMessage()
        {
            return GetMessageType() >= 10000 && GetMessageType() < 20000;
        }
    }
}
