using System;
using System.IO;
using System.Text;

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

        public PacketBuffer(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte ReadByte(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading byte something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            return Buffer[index];
        }
        public byte ReadByte()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading byte something went wrong. Reader Index: " + ReaderIndex + " , Buffer length: " + Buffer.Length);
            }

            return Buffer[ReaderIndex++];
        }

        public byte[] ReadBytes()
        {
            byte[] bytes = new byte[Buffer.Length];
            Buffer.CopyTo(bytes, 0);

            return bytes;
        }

        public byte[] ReadBytes(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading bytes something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            byte[] bytes = new byte[Buffer.Length];
            Buffer.CopyTo(bytes, index);

            return bytes;
        }
        public byte[] ReadBytes(int index, int endIndex)
        {
            if (index + endIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading bytes something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            byte[] bytes = new byte[Buffer.Length];
            Array.Copy(Buffer, index, bytes, 0, endIndex);

            return bytes;
        }

        public void WriteByte(int index, byte value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing byte something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            Buffer[index] = value;
        }
        public void WriteByte(byte value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When reading byte something went wrong. Writer Index: " + WriterIndex + " , Buffer length: " + Buffer.Length);
            }

            Buffer[WriterIndex++] = value;
        }

        public void WriteBytes(byte[] bytes)
        {
            if (WriterIndex + bytes.Length > Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing bytes something went wrong. Writer Index: " + WriterIndex + " , Bytes:" + bytes.Length + " , Buffer length: " + Buffer.Length);
            }

            bytes.CopyTo(Buffer, WriterIndex);
            WriterIndex += bytes.Length;
        }

        public void WriteBytes(int index, byte[] bytes)
        {
            if (index + bytes.Length > Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing bytes something went wrong. Index: " + index + " , Bytes:" + bytes.Length + " , Buffer length: " + Buffer.Length);
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
                throw new InternalBufferOverflowException("When reading short something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            return BitConverter.ToInt16(Buffer, index);
        }

        public short ReadShort()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading short something went wrong. Reader Index: " + ReaderIndex + " , Buffer length: " + Buffer.Length);
            }

            short value = BitConverter.ToInt16(Buffer, ReaderIndex);
            ReaderIndex += 2;

            return value;
        }

        public void WriteShort(int index, short value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing short something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(index, BitConverter.GetBytes(value));
        }
        public void WriteShort(short value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing short something went wrong. Writer Index: " + WriterIndex + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(BitConverter.GetBytes(value));
        }

        public int ReadInteger(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading integer something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            return BitConverter.ToInt32(Buffer, index);
        }

        public int ReadInteger()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading integer something went wrong. Reader Index: " + ReaderIndex + " , Buffer length: " + Buffer.Length);
            }

            int value = BitConverter.ToInt32(Buffer, ReaderIndex);
            ReaderIndex += 4;

            return value;
        }

        public void WriteInteger(int index, int value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing integer something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(index, BitConverter.GetBytes(value));
        }
        public void WriteInteger(int value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing integer something went wrong. Writer Index: " + WriterIndex + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(BitConverter.GetBytes(value));
        }

        public long ReadLong(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading long something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            return BitConverter.ToInt64(Buffer, index);
        }

        public long ReadLong()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading long something went wrong. Reader Index: " + ReaderIndex + " , Buffer length: " + Buffer.Length);
            }

            long value = BitConverter.ToInt64(Buffer, ReaderIndex);
            ReaderIndex += 8;

            return value;
        }

        public void WriteLong(int index, long value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing long something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(index, BitConverter.GetBytes(value));
        }
        public void WriteLong(long value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing long something went wrong. Writer Index: " + WriterIndex + " , Buffer length: " + Buffer.Length);
            }

            WriteBytes(BitConverter.GetBytes(value));
        }

        public string ReadString(int index)
        {
            if (index >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading string something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            int stringLength = ReadInteger(index);

            return Encoding.UTF8.GetString(Buffer, index, stringLength);
        }

        public string ReadString()
        {
            if (ReaderIndex >= Buffer.Length)
            {
                throw new InternalBufferOverflowException("When reading string something went wrong. Reader Index: " + ReaderIndex + " , Buffer length: " + Buffer.Length);
            }

            int stringLength = ReadInteger(ReaderIndex);
            ReaderIndex += 4;

            string str = Encoding.UTF8.GetString(Buffer, ReaderIndex, stringLength);
            ReaderIndex += stringLength;

            return str;
        }

        public void WriteString(int index, string value)
        {
            if (index >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing string something went wrong. Index: " + index + " , Buffer length: " + Buffer.Length);
            }

            byte[] str = Encoding.UTF8.GetBytes(value);

            WriteInteger(index, str.Length);
            WriteBytes(index + 4, str);
        }

        public void WriteString(string value)
        {
            if (WriterIndex >= Buffer.Length)
            {
                throw new IndexOutOfRangeException("When writing string something went wrong. Writer Index: " + WriterIndex + " , Buffer length: " + Buffer.Length);
            }

            byte[] str = Encoding.UTF8.GetBytes(value);

            WriteInteger(str.Length);
            WriteBytes(str);
        }

        public int Lenght() => Buffer.Length;
    }
}
