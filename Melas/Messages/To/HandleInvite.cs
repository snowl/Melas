using System;

namespace Melas.Messages.To
{
    public class HandleInvite : ClientMessage
    {
        private String FriendID;
        private InviteAction Action;

        public HandleInvite(String FriendID, InviteAction Action)
        {
            this.FriendID = FriendID;
            this.Action = Action;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)33); // ID
            writer.Write((byte)Action);
            writer.Write(FriendID);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
