using System;

namespace PacketLibrary.Network
{
    public class ProtocolRegistry
    {

        public Protocol ParentProtocol { get; }
        public CodecContainer Outbound { get; }
        public CodecContainer Inbound { get; }
        public HandlerService Handlers { get; }

        public ProtocolRegistry(Protocol parentProtocol)
        {
            ParentProtocol = parentProtocol;

            Outbound = new CodecContainer();
            Inbound = new CodecContainer();
            Handlers = new HandlerService();
        }

        public void RegisterInbound(int operationalCode, ICodec<Packet> codec)
        {
            Inbound.Bind(operationalCode, codec);
        }

        public void RegisterInboundWithHandler(int operationalCode, ICodec<Packet> codec, Type packetClass, IPacketHandler<Packet> handler)
        {
            Inbound.Bind(operationalCode, codec);
            Handlers.Bind(packetClass, handler);
        }

        public void RegisterOutbound(int operationalCode, ICodec<Packet> codec)
        {
            Outbound.Bind(operationalCode, codec);
        }

        public IPacketHandler<Packet> GetPacketHandler(Type type)
        {
            return Handlers.Find(type);
        }

        public ICodec<Packet> ReadHeader(PacketBuffer buffer)
        {
            int operationalCode = buffer.ReadInteger();
            int length = buffer.ReadInteger();

            return null;
        }

        public void WritePacket(Packet packet, PacketBuffer buffer)
        {
            int operationalCode = Outbound.GetOperationalCode(packet);
            ICodec<Packet> codec = Outbound.Get(operationalCode);

            PacketBuffer data = codec.Encode(packet, new PacketBuffer(1024));

            WriteHeader(operationalCode, data.Lenght(), buffer);
            buffer.WriteBuffer(data);
        }

        public void WriteHeader(int operationalCode, int length, PacketBuffer buffer)
        {
            buffer.WriteInteger(operationalCode);
            buffer.WriteInteger(length);
        }
    }
}
