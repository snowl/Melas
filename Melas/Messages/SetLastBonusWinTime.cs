using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class SetLastBonusWinTime : ServerMessage
    {
        public override byte ID => 51;

        public int LastBonusWinTime { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            LastBonusWinTime = int.Parse(Data.ReadString());
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
