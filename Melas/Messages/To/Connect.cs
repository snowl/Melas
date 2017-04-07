using System;

namespace Melas.Messages.To
{
    public class Connect : ClientMessage
    {
        private String Username;
        private String AuthToken;

        public Connect(String AuthToken)
        {
            String[] TokenSplit = AuthToken.Split('&');
            this.AuthToken = TokenSplit[0].Replace("password=", "");
            this.Username = TokenSplit[1].Replace("username=", "").Trim();
        }

        public override byte[] Serialize()
        {
            ByteWriter writer = new ByteWriter();

            writer.Write((byte)2); //ID 2
            writer.Write((byte)2);
            writer.Write(this.Username);
            writer.Write("*");
            writer.Write(this.AuthToken);
            writer.Write((byte)0);

            return writer.Complete();
        }
    }
}
