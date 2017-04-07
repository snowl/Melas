using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Melas.Client.BrowserBridge
{
    public class Bridge
    {
        private Connection connection;

        public Bridge()
        {
            this.connection = new Connection(Server.Live);
        }
        
        public LoginResult Login(string username, string password)
        {
            LoginResult message = new LoginResult();

            connection.Username = username;
            connection.Password = password;

            message.succeeded = true;

            try
            {
                connection.Connect().Wait(10000);
            }
            catch (TimeoutException e)
            {
                message.message = "Unable to login.";
                message.succeeded = false;
            }
            catch (Exception e)
            {
                message.message = e.Message;
                message.succeeded = false;
            }

            return message;
        }
    }
}
