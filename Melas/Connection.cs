using Hina;
using Melas.Messages;
using Melas.Messages.To;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Melas
{
    public class Connection
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private static readonly HttpClient client = new HttpClient();
        public Server Type { get; private set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Boolean Connected { get; private set; }
        private String AuthToken { get; set; }

        internal CancellationToken readerToken;
        internal CancellationToken writerToken;
        internal Reader reader;
        internal Writer writer;

        public Connection(Server type)
        {
            this.Type = type;
        }

        public async Task<bool> Connect()
        {
            await GetAuthToken();

            String address;
            switch (this.Type)
            {
                case Server.Live: { address = Endpoints.Live; break; }
                case Server.PTR: { address = Endpoints.PTR; break; }
                case Server.Testing: { address = Endpoints.Testing; break; }
                default:
                {
                    throw new Exception("Unable to find endpoint for this server type.");
                }
            }

            TcpClient tcp = new TcpClient() { NoDelay = true };
            await tcp.ConnectAsync(address, Endpoints.Port);
            var stream = tcp.GetStream();

            this.readerToken = new CancellationToken();
            this.reader = new Reader(stream, readerToken);
            this.reader.MessageDeserialized += Reader_MessageDeserialized;
            this.reader.RunAsync().Forget();

            this.writerToken = new CancellationToken();
            this.writer = new Writer(stream, writerToken);
            this.writer.RunAsync().Forget();

            Connect connectPacket = new Connect(this.AuthToken);
            WriteMessage(connectPacket);

            this.Connected = true;
            return true;
        }

        private void Reader_MessageDeserialized(object sender, MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(sender, e);
        }

        public void WriteMessage(ClientMessage message)
        {
            this.writer.QueueWrite(message);
        }

        internal async Task GetAuthToken()
        {
            var values = new Dictionary<string, string>
            {
               { "username", this.Username },
               { "password", this.Password }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(Endpoints.Login, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Contains("emptyError"))
            {
                throw new Exception("No credentials provided");
            }
            else if (responseString.Contains("dberror"))
            {
                throw new Exception("Town of Salem is currently down.");
            }
            else if (responseString.Contains("usererror"))
            {
                throw new Exception("This username doesn't exist.");
            }
            else if (responseString.Contains("pwerror"))
            {
                throw new Exception("The password provided is incorrect.");
            }

            this.AuthToken = responseString;
        }
    }
}