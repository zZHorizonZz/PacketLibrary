using System;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public class ProtocolRegistry
    {

        public Protocol ParentProtocol { get; }
        public CodecContainer<Packet> Outbound { get; }
        public CodecContainer<Packet> Inbound { get; }
        public HandlerService Handlers { get; }

        public ProtocolRegistry(Protocol parentProtocol)
        {
            ParentProtocol = parentProtocol;

            Outbound = new CodecContainer<Packet>();
            Inbound = new CodecContainer<Packet>();
            Handlers = new HandlerService();
        }

        public void RegisterInbound<T>(int operationalCode, ICodec<T> codec) where T : Packet
        {
            Inbound.Bind(operationalCode, (ICodec<Packet>)codec);
        }

        public void RegisterInboundWithHandler<T>(int operationalCode, ICodec<T> codec, Type packetClass, IPacketHandler<T> handler) where T : Packet
        {
            Inbound.Bind(operationalCode, (ICodec<Packet>)codec);
            Handlers.Bind(packetClass, (IPacketHandler<Packet>)handler);
        }

        public void RegisterOutbound<T>(int operationalCode, ICodec<T> codec) where T : Packet
        {
            Outbound.Bind(operationalCode, (ICodec<Packet>)codec);
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

            ICodec<Packet> codec = Inbound.Get(operationalCode);

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
            PacketBuffer buffer = new PacketBuffer(1024);

            int operationalCode = Outbound.GetOperationalCode(packet);
            ICodec<Packet> codec = Outbound.Get(operationalCode);

            PacketBuffer data = codec.Encode(packet, new PacketBuffer(1024));

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
