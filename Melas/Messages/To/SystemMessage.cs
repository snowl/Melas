using System;

namespace Melas.Messages.To
{
    public class SystemMessage : ClientMessage
    {
        private string Message;

        public SystemMessage(String message)
        {
            this.Message = message;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)24); // ID
            writer.Write(Message);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
