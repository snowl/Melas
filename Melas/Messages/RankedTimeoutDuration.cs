using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
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

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
