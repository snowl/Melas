using System;
using System.Collections.Generic;
using System.Linq;

namespace Melas.Messages.From
{
    public class EarnedAchievements : ServerMessage
    {
        public override byte ID => 52;

        public List<int> Achievements { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            Achievements = Data.ReadString().Split(',').Select(Int32.Parse).ToList();
        }
    }
}
