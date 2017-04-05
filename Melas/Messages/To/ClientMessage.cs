namespace Melas.Messages.To
{
    public abstract class ClientMessage
    {
        public abstract byte[] Serialize();
    }
}
