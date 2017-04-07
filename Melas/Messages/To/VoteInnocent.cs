namespace Melas.Messages.To
{
    public class VoteInnocent : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)15);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
