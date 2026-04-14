using System.Net;
using System.Net.Sockets;

namespace ReversedOfClans
{
    public class ServerThread
    {
        private readonly string address;
        private readonly int port;
        private TcpListener? serverSocket;

        public ServerThread(string ip, int port)
        {
            this.address = ip;
            this.port = port;
            try
            {
                var ipAddress = IPAddress.Parse(this.address);
                this.serverSocket = new TcpListener(ipAddress, this.port);
                this.serverSocket.Start(50);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to start server on {address}:{port}");
                Console.Error.WriteLine(e.ToString());
            }
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    Socket client = serverSocket.AcceptSocket();
                    Console.WriteLine("[S] New connection from " + client.RemoteEndPoint.ToString());
                    Task.Run(() => HandleClient(client));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error accepting connection");
                    Console.Error.WriteLine(e.ToString());
                }
            }
        }

        private void HandleClient(Socket client)
        {
            var clientThread = new ClientThread(client);
            clientThread.Run();
        }
    }
}
