namespace PacketLibrary.Network
{
    public interface IPacketHandler<T> where T : Packet
    {

        void Handle(T packet);
    }
}
