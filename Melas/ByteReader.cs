using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Melas
{
    public class ByteReader
    {
        public event EventHandler NeedsMoreData;
        int position;
        int length;
        byte[] buf;

        readonly Encoding encoding;
        
        public ByteReader(byte[] buf, int length)
        {
            this.buf = buf;
            this.length = length;
            this.encoding = Encoding.UTF8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RequireLength(int length)
        {
            if (position + length > this.length)
            {
                if (NeedsMoreData != null)
                    NeedsMoreData.Invoke(null, null);
            }
        }
        
        public int Position
        {
            get => position;
            set
            {
                if (value < 0 || value >= buf.Length)
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                position = value;
            }
        }

        public void ReplaceBuffer(byte[] buf, int length)
        {
            this.buf = buf;
            this.length = length;
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

        public string ReadString()
        {
            int length = -1;
            for (int i = position; i < this.length; i++)
            {
                if (buf[i] == 0)
                {
                    length = i - position;
                    break;
                }
            }

            if (length < 0)
            {
                RequireLength(this.length + 1);
                return ReadString();
            }

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