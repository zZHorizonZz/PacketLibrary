namespace PacketLibrary.Network
{
    public class ClientBootstrap : AClient
    {

        public ClientBootstrap(int port) : base(port)
        {
        }

        public ClientBootstrap(string address, int port) : base(address, port)
        {
        }
    }
}
