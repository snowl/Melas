using Melas.Messages;

namespace Melas
{
    public class MessageReceivedEventArgs
    {
        public readonly ServerMessage Body;

        internal MessageReceivedEventArgs(ServerMessage message)
        {
            this.Body = message;
        }
    }
}
