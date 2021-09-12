using PacketLibrary.Logging;
using System;
using System.Net.Sockets;

namespace PacketLibrary.Network
{
    public class DefaultConnection : IConnection
    {
        public Logger logger = Logger.LOGGER;

        private Protocol CurrentProtocol;
        private readonly TcpClient Client;

        public DefaultConnection(Protocol protocol, TcpClient client)
        {
            CurrentProtocol = protocol;
            Client = client;
        }

        public void Read()
        {
            NetworkStream stream = Client.GetStream();

            try
            {
                while (stream.DataAvailable)
                {
                    Packet packet = CurrentProtocol.ProtocolRegistry.ReadPacket(stream);

                    if (packet != null)
                    {
                        IPacketHandler packetHandler = CurrentProtocol.ProtocolRegistry.GetPacketHandler(packet.GetType());

                        if (packetHandler != null)
                        {
                            packetHandler.Handle(this, packet);
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                logger.Error("Something went wrong when reading data from stream of client.", exception);
            }
            catch (Exception exception)
            {
                logger.Error("Error occured when reading packet.", exception);
            }
        }

        public TcpClient GetClient()
        {
            return Client;
        }

        public Protocol GetCurrentProtocol()
        {
            return CurrentProtocol;
        }

        public void SendPacket(Packet packet) => GetCurrentProtocol().ProtocolRegistry.WritePacket(packet, Client.GetStream());

        public void SetProtocol(Protocol protocol)
        {
            CurrentProtocol = protocol;
        }
    }
}
