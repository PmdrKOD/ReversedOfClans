using System.IO;
using System.Text;
using ReversedOfClans.Logic;

namespace ReversedOfClans.Utils
{
    public abstract class  ChecksumEncoder
    {
        protected MemoryStream buffer;
        protected Messaging Messaging;



        public  ChecksumEncoder(Messaging Messaging = null)
        {
            this.Messaging = Messaging;
            this.buffer = new MemoryStream();
        }

        public void WriteInt(int data)
        {
            WriteInt(data, 4);
        }
        public void writeLong(int v1,int v2)
        {
            new LogicLong(v1,v2).Encode(this);
        }
        public void WriteInt(int data, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = length - 1; i >= 0; i--)
            {
                bytes[i] = (byte)(data & 0xFF);
                data >>= 8;
            }
            buffer.Write(bytes, 0, bytes.Length);
        }

        public void WriteByte(int data)
        {
            WriteInt(data, 1);
        }

        public void WriteString(string? data)
        {
            if (data != null)
            {
                byte[] strBytes = Encoding.UTF8.GetBytes(data);
                WriteInt(strBytes.Length);
                buffer.Write(strBytes, 0, strBytes.Length);
            }
            else
            {
                WriteInt(-1); // 0xFFFFFFFF
            }
        }
    }
}
