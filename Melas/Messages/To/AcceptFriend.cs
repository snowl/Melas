using System;

namespace Melas.Messages.To
{
    public class AcceptFriend : ClientMessage
    {
        private String Name;
        private String ID;

        public AcceptFriend(String Name, String ID)
        {
            this.Name = Name;
            this.ID = ID;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)26); // ID
            writer.Write(Name);
            writer.Write((byte)'*');
            writer.Write(ID);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
