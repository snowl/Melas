using Hina.Threading;
using Melas.Messages;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Melas
{
    class Writer
    {
        readonly CancellationToken token;
        readonly AsyncAutoResetEvent reset;
        readonly ConcurrentQueue<ServerMessage> queue;
        readonly Stream stream;

        public Writer(Stream stream, CancellationToken cancellationToken)
        {
            this.stream = stream;
            this.token = cancellationToken;
            this.reset = new AsyncAutoResetEvent();
            this.queue = new ConcurrentQueue<ServerMessage>();
        }

        public void QueueWrite(ServerMessage message)
        {
            queue.Enqueue(message);
            reset.Set();
        }


        // this method must only be called once
        public async Task RunAsync()
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await WriteOnceAsync();
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

        async Task WriteOnceAsync()
        {
            await reset.WaitAsync();

            while (!token.IsCancellationRequested && queue.TryDequeue(out var message))
            {
                byte[] data = message.Serialize();
                await stream.WriteAsync(data, 0, data.Length, token);
            }
        }
    }
}
