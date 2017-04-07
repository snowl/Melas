namespace Melas.Messages.To
{
    public class RequestPlayerStatistics : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)65);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
