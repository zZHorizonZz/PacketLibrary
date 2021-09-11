using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public class ServerBootstrap : Server
    {
        private SimpleProtocol defaultProtocol;

        public ServerBootstrap(int port) : base(port)
        {

        }

        public ServerBootstrap(string address, int port) : base(address, port)
        {
            defaultProtocol = new SimpleProtocol();
        }

        public override IConnection HandleIncomingConnection()
        {
            TcpClient tcpClient = Listener.AcceptTcpClient();

            IConnection connection = new DefaultConnection(defaultProtocol, tcpClient);
            if (connection != null)
            {
                Logger.Debug("Connection between server and client has been established!");
                Connections.Add(connection);
            }

            return connection;
        }
    }
}
