namespace Melas.Messages.To
{
    public class RequestCauldronStatus : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)75);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
