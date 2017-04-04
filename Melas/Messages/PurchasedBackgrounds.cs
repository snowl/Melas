using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages
{
    public class PurchasedBackgrounds : ServerMessage
    {
        public override byte ID => 45;

        public List<int> Backgrounds { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Backgrounds = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
