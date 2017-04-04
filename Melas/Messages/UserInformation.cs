using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
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

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
