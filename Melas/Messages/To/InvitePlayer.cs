using System;

namespace Melas.Messages.To
{
    public class InvitePlayer : ClientMessage
    {
        private String Name;

        public InvitePlayer(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)32); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
