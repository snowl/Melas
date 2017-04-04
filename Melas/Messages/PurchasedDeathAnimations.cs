using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages
{
    public class PurchasedDeathAnimations : ServerMessage
    {
        public override byte ID => 54;

        public List<int> Animations { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Animations = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
