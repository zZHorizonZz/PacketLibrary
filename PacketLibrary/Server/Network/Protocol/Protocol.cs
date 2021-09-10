namespace PacketLibrary.Network
{
    public abstract class Protocol
    {

        public int ProtocolIdentifier { get; }
        public ProtocolRegistry ProtocolRegistry { get; }

        public Protocol(int protocolIdentifier)
        {
            ProtocolIdentifier = protocolIdentifier;
        }
    }
}
