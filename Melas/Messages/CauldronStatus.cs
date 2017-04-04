using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class CauldronStatus : ServerMessage
    {
        public override byte ID => 196;

        public override void Deserialize(ByteReader Data)
        {
            String cauldron = Data.ReadString();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
