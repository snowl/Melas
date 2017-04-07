using Melas.Structures;
using System;

namespace Melas.Messages.To
{
    public class SendWhisper : ClientMessage
    {
        private String Message;
        private byte PlayerIndex;

        public SendWhisper(String Message, byte PlayerIndex)
        {
            this.Message = Message;
            this.PlayerIndex = PlayerIndex;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)8); // ID
            writer.Write((byte)PlayerIndex);
            writer.Write(Message);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
