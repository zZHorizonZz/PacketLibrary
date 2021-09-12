using System;
using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class HandlerService
    {

        private Dictionary<Type, IPacketHandler> Handlers { get; }

        public HandlerService()
        {
            Handlers = new Dictionary<Type, IPacketHandler>();
        }

        public void Bind(Type packet, IPacketHandler handler)
        {
            Handlers.Add(packet, handler);
        }

        public IPacketHandler Find(Type packet)
        {
            return Handlers[packet];
        }
    }
}
