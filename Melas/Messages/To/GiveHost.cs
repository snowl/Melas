using System;

namespace Melas.Messages.To
{
    public class GiveHost : ClientMessage
    {
        private String Name;

        public GiveHost(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)52); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
