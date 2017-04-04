using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Melas
{
    public class ByteWriter
    {        
        int position;
        byte[] buffer;

        readonly Encoding encoding;
        readonly Encoder encoder;
        readonly byte[] temporary;

        public ByteWriter(int length)
            : this(new byte[length]) { }

        public ByteWriter(byte[] buf)
            : this(buf, Encoding.UTF8) { }

        public ByteWriter(byte[] buf, Encoding encoding)
        {
            this.buffer = buf;
            this.encoding = encoding;
            this.encoder = encoding.GetEncoder();
            this.temporary = new byte[8];
        }

        unsafe void RequireLength(int length)
        {
            var source = buffer;
            var available = source.Length - position;
            var requested = length;
            var required = requested - available;

            if (required > 0)
            {
                var newLength = Math.Max(
                    source.Length * 2,
                    source.Length + required);

                var destination = new byte[newLength];

                if (source.Length > 0 && destination.Length > 0)
                {
                    fixed (byte* pSource = &source[0])
                    fixed (byte* pDestination = destination)
                    {
                        System.Buffer.MemoryCopy(
                            source: pSource,
                            destination: pDestination,
                            destinationSizeInBytes: destination.Length,
                            sourceBytesToCopy: source.Length);
                    }
                }

                buffer = destination;
            }
        }

        void CopyTemporary(int count)
        {
            RequireLength(count);

            for (var i = 0; i < count; i++)
                buffer[i] = temporary[i];

            position += count;
        }

        public bool EndOfStream
        {
            get => position >= buffer.Length;
        }

        public int Length
        {
            get => buffer.Length;
        }

        public int Position
        {
            get { return position; }
            set { if (value < 0 || value >= buffer.Length) throw new ArgumentOutOfRangeException(nameof(value), value, null); position = value; }
        }

        public void Reset()
        {
            position = 0;
        }

        public byte[] Complete()
        {
            return buffer;
        }


        public void WriteBool(bool value) => Write(value);
        public void WriteByte(byte value) => Write(value);
        public void WriteSByte(sbyte value) => Write(value);
        public void WriteBytes(byte[] buffer) => Write(buffer);
        public void WriteBytes(byte[] buffer, int index, int count) => Write(buffer, index, count);
        public void WriteChar(char character) => Write(character);
        public void WriteChars(char[] characters) => Write(characters);
        public void WriteChars(char[] characters, int index, int count) => Write(characters, index, count);
        public void WriteDouble(double value) => Write(value);
        public void WriteInt16(short value) => Write(value);
        public void WriteUInt16(ushort value) => Write(value);
        public void WriteInt32(int value) => Write(value);
        public void WriteUInt32(uint value) => Write(value);
        public void WriteInt64(long value) => Write(value);
        public void WriteUInt64(ulong value) => Write(value);
        public void WriteSingle(float value) => Write(value);
        public void WriteString(string value) => Write(value);


        public void Write(bool value)
        {
            RequireLength(1);
            buffer[position++] = (byte)(value ? 1 : 0);
        }

        public void Write(byte value)
        {
            RequireLength(1);
            buffer[position++] = value;
        }

        public void Write(sbyte value)
        {
            RequireLength(1);
            buffer[position++] = (byte)value;
        }

        public void Write(byte[] buffer)
        {
            WriteBytesInternal(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int index, int count)
        {
            if (index + count > buffer.Length)
                throw new ArgumentOutOfRangeException("index + count exceeds the length of the array");

            WriteBytesInternal(buffer, index, count);
        }

        unsafe void WriteBytesInternal(byte[] buffer, int index, int count)
        {
            RequireLength(count);

            if (buffer.Length > 0 && this.buffer.Length > 0)
            {
                fixed (byte* pSource = &buffer[index])
                fixed (byte* pDestination = &this.buffer[position])
                {
                    System.Buffer.MemoryCopy(
                        source: pSource,
                        destination: pDestination,
                        destinationSizeInBytes: this.buffer.Length - position,
                        sourceBytesToCopy: count);
                }
            }

            position += count;
        }

        public void Write(char character)
        {
            if (char.IsSurrogate(character))
                throw new ArgumentException("can't write singular surrogate character");

            var bytes = encoder.GetBytes(new[] { character }, 0, 1, temporary, 0, true);
            WriteBytesInternal(temporary, 0, bytes);
        }

        public void Write(char[] characters)
        {
            Write(characters, 0, characters.Length);
        }

        public void Write(char[] characters, int index, int count)
        {
            var length = encoding.GetByteCount(characters, index, count);
            RequireLength(length);

            encoding.GetBytes(characters, index, count, buffer, position);
            position += length;
        }

        public unsafe void Write(double value)
        {
            var temp = *(ulong*)&value;
            temporary[0] = (byte)temp;
            temporary[1] = (byte)(temp >> 8);
            temporary[2] = (byte)(temp >> 16);
            temporary[3] = (byte)(temp >> 24);
            temporary[4] = (byte)(temp >> 32);
            temporary[5] = (byte)(temp >> 40);
            temporary[6] = (byte)(temp >> 48);
            temporary[7] = (byte)(temp >> 56);

            CopyTemporary(8);
        }

        public void Write(short value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);

            CopyTemporary(2);
        }

        public void Write(ushort value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);

            CopyTemporary(2);
        }

        public void Write(int value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);
            temporary[2] = (byte)(value >> 16);
            temporary[3] = (byte)(value >> 24);

            CopyTemporary(4);
        }

        public void Write(uint value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);
            temporary[2] = (byte)(value >> 16);
            temporary[3] = (byte)(value >> 24);

            CopyTemporary(4);
        }

        public void Write(long value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);
            temporary[2] = (byte)(value >> 16);
            temporary[3] = (byte)(value >> 24);
            temporary[4] = (byte)(value >> 32);
            temporary[5] = (byte)(value >> 40);
            temporary[6] = (byte)(value >> 48);
            temporary[7] = (byte)(value >> 56);

            CopyTemporary(8);
        }

        public void Write(ulong value)
        {
            temporary[0] = (byte)value;
            temporary[1] = (byte)(value >> 8);
            temporary[2] = (byte)(value >> 16);
            temporary[3] = (byte)(value >> 24);
            temporary[4] = (byte)(value >> 32);
            temporary[5] = (byte)(value >> 40);
            temporary[6] = (byte)(value >> 48);
            temporary[7] = (byte)(value >> 56);

            CopyTemporary(8);
        }

        public unsafe void Write(float value)
        {
            var temp = *(uint*)&value;
            temporary[0] = (byte)temp;
            temporary[1] = (byte)(temp >> 8);
            temporary[2] = (byte)(temp >> 16);
            temporary[3] = (byte)(temp >> 24);

            CopyTemporary(4);
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

        public void ModifyAt(int position, Action action)
        {
            var saved = this.position;
            this.position = position;
            action();
            this.position = saved;
        }

        public void ModifyAt(int position, Action<ByteWriter> action)
        {
            var saved = this.position;
            this.position = position;
            action(this);
            this.position = saved;
        }

        public void CopyTo(ByteWriter writer)
        {
            writer.Write(buffer);
        }

        public void CopyTo(Stream stream)
        {
            stream.Write(buffer, 0, buffer.Length);
        }

        public Task CopyToAsync(Stream stream)
        {
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public Task CopyToAsync(Stream stream, CancellationToken cancellationToken)
        {
            return stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }
    }
}