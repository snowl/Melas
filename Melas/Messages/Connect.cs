using System;
using System.Text;

namespace Melas.Messages
{
    public class Connect : ServerMessage
    {
        private String Username { get; set; }
        private String AuthToken { get; set; }
        private int Length;

        public Connect(String AuthToken)
        {
            String[] TokenSplit = AuthToken.Split('&');
            this.AuthToken = TokenSplit[0].Replace("password=", "");
            this.Username = TokenSplit[1].Replace("username=", "").Trim();
            this.Length = 2 + this.Username.Length + 19 + this.AuthToken.Length + 1;
        }

        public override byte ID => 2;

        public override void Deserialize(ByteReader Data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter(Length);

            writer.Write(ID);
            writer.Write((byte)3);
            writer.Write(this.Username);
            writer.Write("*76561197995252609*");
            writer.Write(this.AuthToken);
            writer.Write((byte)0x0);

            return writer.Complete();
        }
    }
}
