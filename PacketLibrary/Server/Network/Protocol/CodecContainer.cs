using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class CodecContainer
    {

        private SortedDictionary<int, ICodec<Packet>> Codec { get; }
        private SortedDictionary<Packet, int> OperationalCodes { get; }


        public CodecContainer()
        {
            Codec = new SortedDictionary<int, ICodec<Packet>>();
        }

        public void Bind(int operationCode, ICodec<Packet> codec)
        {
            if (HasCodec(operationCode))
            {
                throw new AlreadyRegisteredException("Codec is already assigned under " + operationCode + " operation code.");
            }

            Codec.Add(operationCode, codec);
        }

        public ICodec<Packet> Get(int operationCode) => Codec[operationCode];

        public int GetOperationalCode(Packet packet) => OperationalCodes[packet];

        public bool HasCodec(int operationCode) => Codec.ContainsKey(operationCode);
    }
}
