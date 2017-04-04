using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public class CurrencyMultiplier : ServerMessage
    {
        public override byte ID => 88;

        public int Paid { get; private set; }
        public int Free { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            List<int> multipliers = Data.ReadString().Split('*').Select(Int32.Parse).ToList();
            Paid = multipliers[0];
            Free = multipliers[1];
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
