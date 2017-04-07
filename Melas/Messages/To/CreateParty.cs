namespace Melas.Messages.To
{
    public class CreateParty : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)31);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
