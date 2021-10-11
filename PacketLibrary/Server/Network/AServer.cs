using PacketLibrary.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public abstract class AServer
    {

        public Logger Logger { get; }

        public string Address { get; }
        public int Port { get; }

        private TcpListener Listener;
        public List<IConnection> Connections { get; }

        public AServer(int port) : this("127.0.0.1", port)
        {

        }

        public AServer(string address, int port)
        {
            Address = address;
            Port = port;

            Logger = new Logger();
            Connections = new List<IConnection>();
        }

        public TcpListener Start()
        {
            Logger.Info("Server is starting..", null);

            IPAddress address = IPAddress.Parse(Address);
            Listener = new TcpListener(address, Port);

            try
            {
                Listener.Start();
            }
            catch (SocketException exception)
            {
                Logger.Error("Error occured when starting new tcp listerner.", exception, null);
            }

            return Listener;
        }

        public abstract IConnection HandleIncomingConnection();

        public TcpListener GetListener()
        {
            return Listener;
        }
    }
}
