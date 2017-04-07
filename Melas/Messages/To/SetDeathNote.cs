using System;

namespace Melas.Messages.To
{
    public class SetDeathNote : ClientMessage
    {
        private String DeathNote;

        public SetDeathNote(String DeathNote)
        {
            this.DeathNote = DeathNote;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)18); // ID
            writer.Write(DeathNote);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
