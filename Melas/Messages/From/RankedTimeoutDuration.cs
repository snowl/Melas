using System;

namespace Melas.Messages.From
{
    public class RankedTimeoutDuration : ServerMessage
    {
        public override byte ID => 75;

        public override void Deserialize(ByteReader Data)
        {
            String timeout = Data.ReadString();
            if (timeout.Length == 0)
                return;
            throw new Exception("Handle timeout");
        }
    }
}
