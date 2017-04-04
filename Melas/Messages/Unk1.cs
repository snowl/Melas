using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class Unk1 : ServerMessage
    {
        public override byte ID => 1;

        public override void Deserialize(ByteReader Data)
        {
            byte a = Data.ReadByte(); // 2
            byte b = Data.ReadByte(); // 0
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
