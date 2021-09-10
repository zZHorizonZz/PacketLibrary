using System;
using System.IO;

namespace PacketLibrary.Network
{
    public class PacketBuffer
    {
        public byte[] Buffer { get; }

        public int ReaderIndex { get; set; }
        public int WriterIndex { get; set; }


        public PacketBuffer(int capacity)
        {
            Buffer = new byte[capacity];
        }

        public byte ReadByte(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            return Buffer[index];
        }
        public byte ReadByte()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            return Buffer[ReaderIndex++];
        }

        public void WriteByte(int index, byte value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            Buffer[index] = value;
        }
        public void WriteByte(byte value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            Buffer[WriterIndex++] = value;
        }

        public void WriteBytes(byte[] bytes)
        {
            if (WriterIndex + bytes.Length > Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < bytes.Length; i++)
            {
                Buffer[WriterIndex++ + i] = bytes[i];
            }
        }

        public void WriteBytes(int index, byte[] bytes)
        {
            if (index + bytes.Length > Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < bytes.Length; i++)
            {
                Buffer[index++ + i] = bytes[i];
            }
        }

        public void WriteBuffer(PacketBuffer buffer)
        {
            WriteBytes(buffer.Buffer);
        }

        public short ReadShort(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            return BitConverter.ToInt16(Buffer, index);
        }

        public short ReadShort()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            short value = BitConverter.ToInt16(Buffer, ReaderIndex);
            ReaderIndex += 2;

            return value;
        }

        public void WriteShort(int index, short value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            WriteBytes(index, BitConverter.GetBytes(value));
        }
        public void WriteShort(short value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            WriteBytes(BitConverter.GetBytes(value));
        }

        public int ReadInteger(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            return BitConverter.ToInt32(Buffer, index);
        }

        public int ReadInteger()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException();
            }

            int value = BitConverter.ToInt32(Buffer, ReaderIndex);
            ReaderIndex += 4;

            return value;
        }

        public void WriteInteger(int index, int value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            WriteBytes(index, BitConverter.GetBytes(value));
        }
        public void WriteInteger(int value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException();
            }

            WriteBytes(BitConverter.GetBytes(value));
        }

        public int Lenght() => Buffer.Length;
    }
}
