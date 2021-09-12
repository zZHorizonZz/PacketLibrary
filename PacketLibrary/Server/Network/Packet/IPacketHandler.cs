namespace PacketLibrary.Network
{
    public interface IPacketHandler
    {

        void Handle(IConnection connection, Packet packet);
    }
}
