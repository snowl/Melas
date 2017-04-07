namespace Melas.Messages.To
{
    public class LeaveLobby : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)39);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
