using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class CodecContainer
    {

        private IDictionary<int, ICodec> Codec { get; }
        private IDictionary<Packet, int> OperationalCodes { get; }


        public CodecContainer()
        {
            Codec = new SortedDictionary<int, ICodec>();
            OperationalCodes = new SortedDictionary<Packet, int>();
        }

        public void Bind(int operationCode, ICodec codec)
        {
            if (HasCodec(operationCode))
            {
                throw new AlreadyRegisteredException("Codec is already assigned under " + operationCode + " operation code.");
            }

            Codec.Add(operationCode, codec);
        }

        public ICodec Get(int operationCode) => Codec[operationCode];

        public int GetOperationalCode(Packet packet) => OperationalCodes[packet];

        public bool HasCodec(int operationCode) => Codec.ContainsKey(operationCode);
    }
}
