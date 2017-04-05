using Melas.Structures;
using System;

namespace Melas.Messages.To
{
    public class AddFriend : ClientMessage
    {
        private String Name;

        public AddFriend(String Name)
        {
            this.Name = Name;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)25); // ID
            writer.Write(Name);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
