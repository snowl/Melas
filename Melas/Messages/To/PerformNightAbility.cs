namespace Melas.Messages.To
{
    public class PerformNightAbility : ClientMessage
    {
        private byte? PlayerIdx;

        public PerformNightAbility(byte? PlayerIdx)
        {
            this.PlayerIdx = PlayerIdx;
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)11);
            if (PlayerIdx != null)
                writer.Write((byte)(PlayerIdx + 1));
            else
                writer.Write((byte)30);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
