namespace PacketLibrary.Network
{
    public interface IPacketHandler
    {

        void Handle(Packet packet);
    }
}
