using System;

namespace Melas.Messages.From
{
    public class ActiveEvents : ServerMessage
    {
        public override byte ID => 195;

        public override void Deserialize(ByteReader Data)
        {
            String events = Data.ReadString();
            if (events.Length == 0)
                return;
            throw new Exception("Handle active event");
        }
    }
}
