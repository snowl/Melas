namespace Melas.Messages
{
    public abstract class ServerMessage
    {
        public abstract byte ID { get; }
        public abstract void Deserialize(ByteReader Data);
    }
}
