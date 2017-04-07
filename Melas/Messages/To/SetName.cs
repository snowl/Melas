using System;

namespace Melas.Messages.To
{
    public class SetName : ClientMessage
    {
        private String Name;

        public SetName(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)21); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
