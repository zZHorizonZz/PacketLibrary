using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public class DefaultConnection : IConnection
    {

        private Protocol CurrentProtocol;
        private PacketBuffer Buffer;
        private TcpClient Client;

        public DefaultConnection(Protocol protocol, TcpClient client)
        {
            CurrentProtocol = protocol;
            Client = client;
            Buffer = new PacketBuffer(4096);
        }

        public PacketBuffer GetBuffer()
        {
            return Buffer;
        }

        public TcpClient GetClient()
        {
            return Client;
        }

        public Protocol GetCurrentProtocol()
        {
            return CurrentProtocol;
        }

        public void SendPacket(Packet packet) => GetCurrentProtocol().ProtocolRegistry.WritePacket(packet, GetBuffer());

        public void SetProtocol(Protocol protocol)
        {
            CurrentProtocol = protocol;
        }
    }
}
