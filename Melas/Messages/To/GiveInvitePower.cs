using System;

namespace Melas.Messages.To
{
    public class GiveInvitePower : ClientMessage
    {
        private String Name;

        public GiveInvitePower(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)54); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
