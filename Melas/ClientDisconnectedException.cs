using System;

namespace Melas
{
    public class ClientDisconnectedException : Exception
    {
        public ClientDisconnectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
