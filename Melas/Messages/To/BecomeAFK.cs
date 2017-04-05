namespace Melas.Messages.To
{
    public class SendAFKStatus : ClientMessage
    {
        private AFKStatus status;

        public SendAFKStatus(AFKStatus status)
        {
            this.status = status;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)38);
            writer.Write((byte)status);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
