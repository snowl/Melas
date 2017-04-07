using Melas.Structures;
using System;

namespace Melas.Messages.To
{
    public class SendFriendMessage : ClientMessage
    {
        private Friend Friend;
        private string Message;

        public SendFriendMessage(Friend friend, String message)
        {
            this.Friend = friend;
            this.Message = message;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)29); // ID
            writer.Write(Friend.Name);
            writer.Write((byte)'*');
            writer.Write(Message);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
