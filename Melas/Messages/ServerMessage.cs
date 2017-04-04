using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Messages
{
    public abstract class ServerMessage
    {
        public abstract byte ID { get; }

        public abstract void Deserialize(ByteReader Data);

        public abstract byte[] Serialize();
    }
}
