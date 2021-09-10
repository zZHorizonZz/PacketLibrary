using System.Net.Sockets;
using System.Threading.Tasks;

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
            IConnection connection = null;

            Task<TcpClient> client = Listener.AcceptTcpClientAsync();
            client.ContinueWith(tcpClient =>
            {
                connection = new DefaultConnection(defaultProtocol, tcpClient.Result);
            });

            if (connection != null)
            {
                Logger.Debug("Connection between server and client has been established!");
                Connections.Add(connection);
            }

            return connection;
        }
    }
}
