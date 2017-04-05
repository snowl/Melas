using System;

namespace Melas.Messages.From
{
    public class Unk1 : ServerMessage
    {
        public override byte ID => 1;

        public override void Deserialize(ByteReader Data)
        {
            byte a = Data.ReadByte(); // 2
            byte b = Data.ReadByte(); // 0
        }
    }
}
