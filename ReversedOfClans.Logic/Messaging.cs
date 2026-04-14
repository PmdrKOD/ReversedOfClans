using System.Net.Sockets;
using ReversedOfClans.CryptoRC4;
using ReversedOfClans.Packets.Factory;

namespace ReversedOfClans.Logic
{
    public class Messaging
    {
        public string? AndroidID { get; set; }
        public string? MessagingModel { get; set; }


        private readonly Socket socket;
        private readonly CryptoRc4 crypto;
        private NetworkStream? stream;

        public NetworkStream Stream => stream ??= new NetworkStream(socket);

        public Messaging(Socket socket)
        {
            this.socket = socket;
            this.crypto = new CryptoRc4();
        }

        public void SendData(int id, byte[] data, int? version = null)
        {
            byte[] encrypted = crypto.Encrypt(data);
            byte[] packetID = ToBytes(id, 2);
            byte[] packetVersion = version.HasValue ? ToBytes(version.Value, 2) : ToBytes(0, 2);
            byte[] lengthBytes = ToBytes(encrypted.Length, 3);
            byte[] packet = Concat(packetID, lengthBytes, packetVersion, encrypted);

            try
            {
                Stream.Write(packet, 0, packet.Length);
                Stream.Flush();
            }
            catch (IOException e)
            {
                Console.Error.WriteLine($"[D] Failed to send data for packet {id}");
                Console.Error.WriteLine(e.ToString());
            }
            Console.WriteLine($"[C] {id} sent");
        }

        public byte[] Decrypt(byte[] data)
        {
            return crypto.Decrypt(data);
        }

        public void ProcessPacket(int packetID, byte[] payload)
        {
            Console.WriteLine($"[S] {packetID} received");
            try
            {
                byte[] decrypted = Decrypt(payload);
                var factory = LogicMagicMessageFactory.GetFactory(packetID);
                if (factory != null)
                {
                    PiranhaMessage message = factory(decrypted, this);
                    message.Decode();
                    message.Process();
                }
                else
                {
                    Console.WriteLine($"[S] {packetID} not handled");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[S] Error while decrypting / handling {packetID}");
                Console.Error.WriteLine(e.ToString());
            }
        }

        public static byte[] ToBytes(int value, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = length - 1; i >= 0; i--)
            {
                bytes[i] = (byte)(value & 0xFF);
                value >>= 8;
            }
            return bytes;
        }

        public static byte[] Concat(params byte[][] arrays)
        {
            int totalLength = 0;
            foreach (byte[] arr in arrays)
            {
                totalLength += arr.Length;
            }
            byte[] result = new byte[totalLength];
            int currentPos = 0;
            foreach (byte[] arr in arrays)
            {
                Buffer.BlockCopy(arr, 0, result, currentPos, arr.Length);
                currentPos += arr.Length;
            }
            return result;
        }
    }
}
