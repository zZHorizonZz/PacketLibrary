namespace PacketLibrary.Network
{
    public interface ICodec<T> where T : Packet
    {
        T Decode(PacketBuffer buffer);

        PacketBuffer Encode(T packet, PacketBuffer buffer);
    }
}
