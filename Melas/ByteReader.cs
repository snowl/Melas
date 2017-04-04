using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Melas
{
    public class ByteReader
    {
        int position;
        byte[] buf;

        readonly Encoding encoding;

        public ByteReader()
            : this(new byte[0]) { }

        public ByteReader(byte[] buf)
        {
            this.buf = buf;
            this.encoding = Encoding.UTF8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RequireLength(int length)
        {
            if (position + length > buf.Length)
                throw new EndOfStreamException();
        }

        public bool HasLength(int count)
        {
            return position + count <= buf.Length;
        }

        public bool EndOfStream
        {
            get => position >= buf.Length;
        }

        public byte[] Buffer
        {
            get { return buf; }
            set { buf = value; position = 0; }
        }

        public int Length
        {
            get => buf.Length;
        }

        public int Position
        {
            get => position;
            set { if (value < 0 || value >= buf.Length) throw new ArgumentOutOfRangeException(nameof(value), value, null); position = value; }
        }

        public void Reset()
        {
            position = 0;
        }

        public bool ReadBoolean()
        {
            RequireLength(1);
            return buf[position++] == 2;
        }

        public byte ReadByte()
        {
            RequireLength(1);
            return buf[position++];
        }

        public sbyte ReadSByte()
        {
            RequireLength(1);
            return (sbyte)buf[position++];
        }

        public short ReadInt16()
        {
            RequireLength(2);

            var value = (short)(buf[position + 0] | buf[position + 1] << 8);

            position += 2;
            return value;
        }

        public ushort ReadUInt16()
        {
            RequireLength(2);

            var value = (ushort)(buf[position + 0] | buf[position + 1] << 8);

            position += 2;
            return value;
        }

        public int ReadInt32()
        {
            RequireLength(4);

            var value = (int)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);

            position += 4;
            return value;
        }

        public uint ReadUInt32()
        {
            RequireLength(4);

            var value = (uint)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);

            position += 4;
            return value;
        }

        public long ReadInt64()
        {
            RequireLength(8);

            var lo = (uint)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);
            var hi = (uint)(buf[position + 4] | buf[position + 5] << 8 | buf[position + 6] << 16 | buf[position + 7] << 24);
            var value = (long)(ulong)hi << 32 | lo;

            position += 8;
            return value;
        }

        public ulong ReadUInt64()
        {
            RequireLength(8);

            var lo = (uint)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);
            var hi = (uint)(buf[position + 4] | buf[position + 5] << 8 | buf[position + 6] << 16 | buf[position + 7] << 24);
            var value = (ulong)hi << 32 | lo;

            position += 8;
            return value;
        }

        public unsafe float ReadSingle()
        {
            RequireLength(4);

            var value = (uint)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);

            position += 4;
            return *(float*)&value;
        }

        public unsafe double ReadDouble()
        {
            RequireLength(8);

            var lo = (uint)(buf[position + 0] | buf[position + 1] << 8 | buf[position + 2] << 16 | buf[position + 3] << 24);
            var hi = (uint)(buf[position + 4] | buf[position + 5] << 8 | buf[position + 6] << 16 | buf[position + 7] << 24);
            var value = (ulong)hi << 32 | lo;

            position += 8;
            return *(double*)&value;

        }

        public string ReadString()
        {
            int length = 0;
            for (int i = position; i < buf.Length; i++)
            {
                if (buf[i] == 0)
                {
                    length = i - position;
                    break;
                }
            }

            if (length < 0)
                throw new IOException("invalid string length", new FormatException());

            if (length == 0)
            {
                ReadByte();
                return string.Empty;
            }

            RequireLength(length);

            var content = encoding.GetString(buf, position, length);
            position += length + 1;
            return content;
        }
    }
}