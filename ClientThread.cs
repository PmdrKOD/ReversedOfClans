using System.Net.Sockets;
using ReversedOfClans.Logic;
using ReversedOfClans.Packets.Factory;

namespace ReversedOfClans
{
    public class ClientThread
    {
        private readonly Socket client;
        private readonly Messaging Messaging;

        public ClientThread(Socket client)
        {
            this.client = client;
            this.Messaging = new Messaging(client);
        }

        private byte[] RecvAll(NetworkStream stream, int size)
        {
            byte[] data = new byte[size];
            int totalRead = 0;
            while (totalRead < size)
            {
                int read = stream.Read(data, totalRead, size - totalRead);
                if (read == 0)
                {
                    throw new EndOfStreamException();
                }
                totalRead += read;
            }
            return data;
        }

        public void Run()
        {
           
            var cts = new System.Threading.CancellationTokenSource();
            try
            {
                using (client)
                using (var stream = Messaging.Stream)
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        byte[] header;
                        try
                        {
                            header = RecvAll(stream, 7);
                        }
                        catch (EndOfStreamException)
                        {
                            Console.WriteLine("[C] Client disconnected gracefully.");
                            break;
                        }

                        int packetID = ((header[0] & 0xFF) << 8) | (header[1] & 0xFF);
                        int length = ((header[2] & 0xFF) << 16) | ((header[3] & 0xFF) << 8) | (header[4] & 0xFF);
                        int version = ((header[5] & 0xFF) << 8) | (header[6] & 0xFF);

                        byte[] data = RecvAll(stream, length);

                        if (length == data.Length)
                        {
                            Console.WriteLine($"[C] {packetID} received.");
                            try
                            {
                                byte[] decrypted = Messaging.Decrypt(data);
                                var factory = LogicMagicMessageFactory.GetFactory(packetID);
                                if (factory != null)
                                {
                                    PiranhaMessage message = factory(decrypted, Messaging);
                                    message.Decode();
                                    message.Process();
                                }
                                else
                                {
                                    Console.WriteLine($"[C] {packetID} not handled.");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine($"[C] Error while decrypting / handling {packetID}");
                                Console.Error.WriteLine(e.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[C] Incorrect Length for packet {packetID} (header length: {length}, data length: {data.Length}).");
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("[C] IO Error with client connection");
                Console.Error.WriteLine(e.ToString());
            }
        }
    }
}
