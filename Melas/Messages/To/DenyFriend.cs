using System;

namespace Melas.Messages.To
{
    public class DenyFriend : ClientMessage
    {
        private String ID;

        public DenyFriend(String ID)
        {
            this.ID = ID;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)28); // ID
            writer.Write(ID);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
