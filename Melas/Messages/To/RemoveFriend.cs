using Melas.Structures;
using System;

namespace Melas.Messages.To
{
    public class RemoveFriend : ClientMessage
    {
        private Friend Friend;
        private int Length;

        public RemoveFriend(Friend friend)
        {
            this.Friend = friend;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)27); // ID
            writer.Write(Friend.Name);
            writer.Write("*");
            writer.Write(Friend.ID.ToString());
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
