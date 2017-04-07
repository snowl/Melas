using System;
using System.Text;

namespace Melas
{
    public class ByteWriter
    {        
        int position;
        byte[] buffer;

        readonly Encoding encoding;

        public ByteWriter()
            : this(new byte[0]) { }

        public ByteWriter(byte[] buf)
            : this(buf, Encoding.UTF8) { }

        public ByteWriter(byte[] buf, Encoding encoding)
        {
            this.buffer = buf;
            this.encoding = encoding;
        }

        void RequireLength(int length)
        {
            var source = buffer;
            var available = source.Length - position;
            var requested = length;
            var required = requested - available;

            if (required > 0)
            {
                var newLength = source.Length + required;

                var destination = new byte[newLength];

                if (source.Length > 0 && destination.Length > 0)
                {
                    Buffer.BlockCopy(buffer, 0, destination, 0, source.Length);
                }

                buffer = destination;
            }
        }

        public byte[] Complete()
        {
            return buffer;
        }

        public void Write(byte value)
        {
            RequireLength(1);
            buffer[position++] = value;
        }

        public void Write(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var count = value.Length;
            var length = encoding.GetByteCount(value);
            
            RequireLength(length);

            encoding.GetBytes(value, 0, count, buffer, position);
            position += length;
        }
    }
}