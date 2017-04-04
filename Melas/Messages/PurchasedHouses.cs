using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages
{
    public class PurchasedHouses : ServerMessage
    {
        public override byte ID => 44;

        public List<int> Houses { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Houses = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
