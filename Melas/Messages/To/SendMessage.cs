using System;

namespace Melas.Messages.To
{
    public class SendMessage : ClientMessage
    {
        private String Message;

        public SendMessage(String Message)
        {
            this.Message = Message;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)3); // ID
            writer.Write(Message);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
