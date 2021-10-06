using System;
using System.Collections.Generic;

namespace PacketLibrary.Network
{
    public class CodecContainer
    {

        private IDictionary<int, ICodec> Codec { get; }
        private IDictionary<Type, int> OperationalCodes { get; }


        public CodecContainer()
        {
            Codec = new Dictionary<int, ICodec>();
            OperationalCodes = new Dictionary<Type, int>();
        }

        public void Bind(int operationCode, ICodec codec, Type type)
        {
            if (HasCodec(operationCode))
            {
                throw new AlreadyRegisteredException("Codec is already assigned under " + operationCode + " operation code.");
            }

            Codec.Add(operationCode, codec);
            OperationalCodes.Add(type, operationCode);
        }

        public ICodec Get(int operationCode) => Codec[operationCode];

        public int GetOperationalCode(Type packet) => OperationalCodes[packet];

        public bool HasCodec(int operationCode) => Codec.ContainsKey(operationCode);
    }
}
