using System;

namespace Melas.Messages.To
{
    public class SetLastWill : ClientMessage
    {
        private String LastWill;

        public SetLastWill(String LastWill)
        {
            this.LastWill = LastWill;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)17); // ID
            writer.Write(LastWill);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
