using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
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

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
