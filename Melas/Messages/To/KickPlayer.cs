using System;

namespace Melas.Messages.To
{
    public class KickPlayer : ClientMessage
    {
        private String Name;

        public KickPlayer(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)53); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
