namespace Melas.Messages.To
{
    public class StartLobby : ClientMessage
    {
        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)9);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
