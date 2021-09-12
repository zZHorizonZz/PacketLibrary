using System;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    /**
     * Protocol registry is something like storage for outbound and inbound
     * codecs that are used for <see cref="Packet"/> decoding and encoding
     * of packets. Also there is <see cref="HandlerService"/> that is used
     * for registration of <see cref="IPacketHandler{T}"/> that can handle
     * method that should be executed after incoming <see cref="Packet"/>
     * is successfully decoded.
     */
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

        /**
         * 
         */
        public void RegisterInbound(int operationalCode, ICodec codec, Type packet)
        {
            Inbound.Bind(operationalCode, codec, packet);
        }

        public void RegisterInboundWithHandler(int operationalCode, ICodec codec, Type packet, IPacketHandler<Packet> handler)
        {
            Inbound.Bind(operationalCode, codec, packet);
            Handlers.Bind(packet, handler);
        }

        public void RegisterOutbound(int operationalCode, ICodec codec, Type packet)
        {
            Outbound.Bind(operationalCode, codec, packet);
        }

        public IPacketHandler<Packet> GetPacketHandler(Type type)
        {
            return Handlers.Find(type);
        }

        public Packet ReadPacket(NetworkStream stream)
        {
            Tuple<int, PacketBuffer> header = ReadHeader(stream);

            int operationalCode = header.Item1;
            PacketBuffer buffer = header.Item2;

            ICodec codec = Inbound.Get(operationalCode);

            return codec.Decode(buffer);
        }

        public Tuple<int, PacketBuffer> ReadHeader(NetworkStream stream)
        {
            byte[] header = new byte[8];
            stream.Read(header, 0, header.Length);

            int operationalCode = BitConverter.ToInt32(header, 0);
            int length = BitConverter.ToInt32(header, 4);

            return new Tuple<int, PacketBuffer>(operationalCode, new PacketBuffer(length));
        }

        public void WritePacket(Packet packet, NetworkStream stream)
        {
            PacketBuffer buffer = new PacketBuffer(1028);

            int operationalCode = Outbound.GetOperationalCode(packet.GetType());
            ICodec codec = Outbound.Get(operationalCode);

            PacketBuffer data = codec.Encode(packet, new PacketBuffer(1020));

            WriteHeader(operationalCode, data.Lenght(), buffer);
            buffer.WriteBuffer(data);

            stream.Write(buffer.Buffer, 0, buffer.Lenght());
        }

        public void WriteHeader(int operationalCode, int length, PacketBuffer buffer)
        {
            buffer.WriteInteger(operationalCode);
            buffer.WriteInteger(length);
        }
    }
}
