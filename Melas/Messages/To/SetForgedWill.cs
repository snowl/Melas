using System;

namespace Melas.Messages.To
{
    public class SetForgedWill : ClientMessage
    {
        private String ForgedWill;

        public SetForgedWill(String ForgedWill)
        {
            this.ForgedWill = ForgedWill;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)64); // ID
            writer.Write(ForgedWill);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
