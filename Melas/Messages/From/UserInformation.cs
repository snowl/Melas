using System;

namespace Melas.Messages.From
{
    public class UserInformation : ServerMessage
    {
        public override byte ID => 28;

        public String AccountName { get; private set; }
        public int PaidCurrency { get; private set; }
        public int FreeCurrency { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            String[] data = Data.ReadString().Split('*');
            AccountName = data[0];
            PaidCurrency = int.Parse(data[1]);
            FreeCurrency = int.Parse(data[2]);
        }
    }
}
