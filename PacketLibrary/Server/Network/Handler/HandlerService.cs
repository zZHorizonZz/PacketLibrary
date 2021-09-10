using System;
using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class HandlerService
    {

        private Dictionary<Type, IPacketHandler<Packet>> Handlers { get; }

        public HandlerService()
        {
            Handlers = new Dictionary<Type, IPacketHandler<Packet>>();
        }

        public void Bind(Type packet, IPacketHandler<Packet> handler)
        {
            Handlers.Add(packet, handler);
        }

        public IPacketHandler<Packet> Find(Type packet)
        {
            return Handlers[packet];
        }
    }
}
