namespace Melas.Messages.To
{
    public class StartParty : ClientMessage
    {
        private GameMode Mode;

        public StartParty(GameMode Mode)
        {
            this.Mode = Mode;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)35);
            writer.Write((byte)Mode);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
