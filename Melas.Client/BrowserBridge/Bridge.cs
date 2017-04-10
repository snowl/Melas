using CefSharp;
using CefSharp.Wpf;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Melas.Client.BrowserBridge
{
    public class Bridge
    {
        private FlashWindow flashWindow;
        private ChromiumWebBrowser browser;
        private Connection connection;

        public Bridge(ChromiumWebBrowser browser)
        {
            this.browser = browser;
            this.flashWindow = new FlashWindow(Application.Current);
        }

        private void Connection_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            browser.GetMainFrame().ExecuteJavaScriptAsync("__bridge.pushMessage(" + JsonConvert.SerializeObject(e.Body) + ")");
        }

        public void Focus()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Application.Current.MainWindow.Activate();
            }));
        }

        public void Flash()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                flashWindow.FlashApplicationWindow();
            }));
        }

        public string GetSavedCredentials()
        {
            return JsonConvert.SerializeObject(SavedUser.GetUser());
        }

        public void Logout()
        {
            connection.Close();
            connection.MessageReceived -= Connection_MessageReceived;
        }

        public void Login(string username, string password, bool save, IJavascriptCallback callback)
        {
            Task.Run(async () =>
            {
                this.connection = new Connection(Server.Live);
                this.connection.MessageReceived += Connection_MessageReceived;

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

                // Save the user if the login succeeded, otherwise
                // delete the info from the file.
                if (message.succeeded && save)
                {
                    SavedUser.SaveUser(new User()
                    {
                        Username = username,
                        Password = password
                    });
                }
                else
                {
                    SavedUser.SaveUser(null);
                }

                using (callback)
                {
                    await callback.ExecuteAsync(JsonConvert.SerializeObject(message));
                }
            });
        }
    }
}
