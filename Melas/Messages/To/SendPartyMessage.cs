using System;

namespace Melas.Messages.To
{
    public class SendPartyMessage : ClientMessage
    {
        private String Message;

        public SendPartyMessage(String Message)
        {
            this.Message = Message;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)36); // ID
            writer.Write(Message);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
