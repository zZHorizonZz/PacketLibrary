using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public interface IConnection
    {
        TcpClient GetClient();

        Protocol GetCurrentProtocol();

        void SetProtocol(Protocol protocol);

        void SendPacket(Packet packet);

        void Read();
    }
}
