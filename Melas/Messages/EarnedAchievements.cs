using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages
{
    public class EarnedAchievements : ServerMessage
    {
        public override byte ID => 52;

        public List<int> Achievements { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            String str = Data.ReadString();
            Achievements = str.Split(',').Select(Int32.Parse).ToList();
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
