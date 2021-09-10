using System;

namespace PacketLibrary.Network
{
    public class AlreadyRegisteredException : SystemException
    {
        public AlreadyRegisteredException()
        {

        }

        public AlreadyRegisteredException(string message) : base(message)
        {

        }
    }
}
