using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public class ServerBootstrap : AServer
    {
        public SimpleProtocol DefaultProtocol { get; }

        public ServerBootstrap(int port) : base(port)
        {

        }

        public ServerBootstrap(string address, int port) : base(address, port)
        {
            DefaultProtocol = new SimpleProtocol();
        }

        public override IConnection HandleIncomingConnection()
        {
            TcpClient tcpClient = GetListener().AcceptTcpClient();

            IConnection connection = new DefaultConnection(DefaultProtocol, tcpClient);
            if (connection != null)
            {
                Logger.Debug("Connection between server and client has been established!");
                Connections.Add(connection);
            }

            return connection;
        }
    }
}
