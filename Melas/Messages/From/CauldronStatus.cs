using System;

namespace Melas.Messages.From
{
    public class CauldronStatus : ServerMessage
    {
        public override byte ID => 196;

        public override void Deserialize(ByteReader Data)
        {
            String cauldron = Data.ReadString();
        }
    }
}
