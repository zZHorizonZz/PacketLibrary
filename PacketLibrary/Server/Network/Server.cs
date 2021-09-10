using PacketLibrary.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public abstract class Server
    {

        public Logger Logger { get; }

        public string Address { get; set; }
        public int Port { get; set; }

        public TcpListener Listener { get; }
        public ProtocolRegistry Protocols { get; }
        public List<IConnection> Connections { get; }

        public Server(int port) : this(null, port)
        {

        }
        public Server(string address, int port)
        {
            Address = address;
            Port = port;

            Logger = new Logger();
            Connections = new List<IConnection>();
        }

        public TcpListener Build()
        {

            Logger.Info("Server is starting..", null);
            TcpListener listener;

            IPAddress address = IPAddress.Parse(Address);
            listener = new TcpListener(address, Port);

            try
            {
                listener.Start();
            }
            catch (SocketException exception)
            {
                Logger.Error("Error occured when starting new tcp listerner.", exception, null);
            }

            return listener;
        }

        public abstract IConnection HandleIncomingConnection();
    }
}
