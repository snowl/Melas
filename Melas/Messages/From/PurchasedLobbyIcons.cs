using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages.From
{
    public class PurchasedLobbyIcons : ServerMessage
    {
        public override byte ID => 53;

        public List<int> Icons { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Icons = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }
    }
}
