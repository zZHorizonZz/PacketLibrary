using PacketLibrary.Logging;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public abstract class AClient
    {
        public Logger Logger = Logger.LOGGER;

        public string Address { get; }
        public int Port { get; }

        private TcpClient Client;

        public AClient(int port) : this("127.0.0.1", port)
        {

        }

        public AClient(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public TcpClient Connect()
        {
            Logger.Info("Connecting to server...");
            Client = new TcpClient(Address, Port);

            if (Client.Connected)
            {
                Logger.Info("Connection to the server has been successfully established.");
            }
            else
            {
                Logger.Warn("Connection to the server failed.");
            }

            return Client;
        }

        public TcpClient GetClient()
        {
            return Client;
        }
    }
}
