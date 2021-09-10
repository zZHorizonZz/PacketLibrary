using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public interface IConnection
    {
        TcpClient GetClient();

        PacketBuffer GetBuffer();

        Protocol GetCurrentProtocol();

        void SetProtocol(Protocol protocol);

        void SendPacket(Packet packet);
    }
}
