using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages
{
    public class PurchasedCharacters : ServerMessage
    {
        public override byte ID => 43;

        public List<int> Characters { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Characters = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
