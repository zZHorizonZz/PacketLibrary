using System;

namespace PacketLibrary.Logging
{
    interface ErrorHandler : IHandler
    {
        void OnException(Exception exception);
    }
}
