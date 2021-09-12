namespace PacketLibrary.Network
{
    public interface ICodec
    {
        Packet Decode(PacketBuffer buffer);

        PacketBuffer Encode(Packet packet, PacketBuffer buffer);
    }
}
