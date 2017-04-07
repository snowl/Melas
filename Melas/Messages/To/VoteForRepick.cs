namespace Melas.Messages.To
{
    public class VoteForRepick : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)23);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
