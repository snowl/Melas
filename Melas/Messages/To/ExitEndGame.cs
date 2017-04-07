namespace Melas.Messages.To
{
    public class ExitEndGame : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)63);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
