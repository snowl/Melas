namespace Melas.Messages.To
{
    public class VoteGuilty : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)14);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
