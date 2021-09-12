using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class CodecContainer<T> where T : Packet
    {

        private SortedDictionary<int, ICodec<T>> Codec { get; }
        private SortedDictionary<T, int> OperationalCodes { get; }


        public CodecContainer()
        {
            Codec = new SortedDictionary<int, ICodec<T>>();
        }

        public void Bind(int operationCode, ICodec<T> codec)
        {
            if (HasCodec(operationCode))
            {
                throw new AlreadyRegisteredException("Codec is already assigned under " + operationCode + " operation code.");
            }

            Codec.Add(operationCode, codec);
        }

        public ICodec<T> Get(int operationCode) => Codec[operationCode];

        public int GetOperationalCode(T packet) => OperationalCodes[packet];

        public bool HasCodec(int operationCode) => Codec.ContainsKey(operationCode);
    }
}
