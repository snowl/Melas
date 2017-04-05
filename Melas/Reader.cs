using Melas.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Melas
{
    class Reader
    {
        public event EventHandler<MessageReceivedEventArgs> MessageDeserialized;
        const int DefaultBufferLength = 4192;
        
        readonly CancellationToken token;
        readonly Stream stream;

        // an intermediate processing buffer of data read from `stream`. this is always at least `chunkLength` bytes large.
        byte[] buffer;

        // the number of bytes available in `buffer`
        int available;

        Dictionary<byte, Type> PacketTypes = new Dictionary<byte, Type>();
        
        public Reader(Stream stream, CancellationToken cancellationToken)
        {
            this.stream = stream;
            this.token = cancellationToken;
            this.buffer = new byte[DefaultBufferLength];
            this.available = 0;

            var listOfMessages = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(ServerMessage).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();

            foreach (Type t in listOfMessages)
            {
                if (t == typeof(ServerMessage))
                    continue;
                ServerMessage instance = (ServerMessage)Activator.CreateInstance(t);
                PacketTypes.Add(instance.ID, t);
            }
        }

        // this method must only be called once
        public async Task RunAsync()
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await ReadOnceAsync();
                }
                catch (TaskCanceledException)
                {
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception thrown.");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        async Task ReadOnceAsync()
        {
            // read a bunch of bytes from the remote server
            await ReadFromStreamAsync();

            // then, read all frames, chunks and messages that are complete within it
            while (available > 0)
                ReadFramesFromBuffer();
        }

        async Task ReadFromStreamAsync()
        {
            var read = await stream.ReadAsync(buffer, available, 128);

            if (read == 0)
                throw new EndOfStreamException("rtmp connection was closed by the remote peer");

            available += read;
        }

        void ReadFramesFromBuffer()
        {
            // the index that we have successfully read into `buffer`. that is, all bytes before this have been
            // successfully read and processed into a valid packet.
            var index = ReadMessage();

            // then, shift unread bytes back to the start of the array
            if (index > 0)
            {
                if (available != index)
                {
                    Buffer.BlockCopy(buffer, index, buffer, 0, available - index);
                }

                available -= index;
            }
        }

        // Returns the bytes read
        int ReadMessage()
        {
            ByteReader reader = new ByteReader(buffer, available);
            reader.NeedsMoreData += async (s, e) =>
            {
                await ReadFromStreamAsync();
                reader.ReplaceBuffer(buffer, available);
            };
            byte ID = reader.ReadByte();

            Type type = PacketTypes[ID];
            if (type == null)
                Console.WriteLine("Got packet ID " + ID + "!");

            ServerMessage instance = (ServerMessage)Activator.CreateInstance(type);
            instance.Deserialize(reader);

            if (MessageDeserialized != null)
            {
                MessageDeserialized.Invoke(null, new MessageReceivedEventArgs(instance));
            }

            return reader.Position;
        }
    }
}
