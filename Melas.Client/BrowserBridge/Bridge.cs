using CefSharp;
using CefSharp.Wpf;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Melas.Client.BrowserBridge
{
    public class Bridge
    {
        private ChromiumWebBrowser browser;
        private Connection connection;

        public Bridge(ChromiumWebBrowser browser)
        {
            this.browser = browser;
            this.connection = new Connection(Server.Live);
            this.connection.MessageReceived += Connection_MessageReceived;
        }

        private void Connection_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            browser.GetMainFrame().ExecuteJavaScriptAsync("__bridge.pushMessage(" + JsonConvert.SerializeObject(e.Body) + ")");
        }

        public void Login(string username, string password, IJavascriptCallback callback)
        {
            Task.Run(async () =>
            {
                LoginResult message = new LoginResult();

                connection.Username = username;
                connection.Password = password;

                message.succeeded = true;

                try
                {
                    await connection.Connect();
                }
                catch (Exception e)
                {
                    message.message = e.Message.ToLower().Replace(".", "");
                    message.succeeded = false;
                }

                using (callback)
                {
                    await callback.ExecuteAsync(JsonConvert.SerializeObject(message));
                }
            });
        }
    }
}
