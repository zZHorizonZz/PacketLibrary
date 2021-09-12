namespace PacketLibrary.Network
{
    /**
     * Protocol represent data that are used for packet decoding and ecoding.
     * For example there is <see cref="ProtocolRegistry"/> and identifier of this
     * protocol.
     */
    public abstract class Protocol
    {

        public int ProtocolIdentifier { get; }
        public ProtocolRegistry ProtocolRegistry { get; }

        public Protocol(int protocolIdentifier)
        {
            ProtocolIdentifier = protocolIdentifier;
            ProtocolRegistry = new ProtocolRegistry(this);
        }
    }
}
