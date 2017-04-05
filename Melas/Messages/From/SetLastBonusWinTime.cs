using System;

namespace Melas.Messages.From
{
    public class SetLastBonusWinTime : ServerMessage
    {
        public override byte ID => 51;

        public int LastBonusWinTime { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            LastBonusWinTime = int.Parse(Data.ReadString());
        }
    }
}
