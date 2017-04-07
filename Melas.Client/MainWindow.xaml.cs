using CefSharp;
using CefSharp.Wpf;
using Melas.Client.BrowserBridge;
using System.Windows;
using System.Windows.Controls;

namespace Melas.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser browser;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.F12:
                    browser.ShowDevTools();
                    break;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CefSettings settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme()
            {
                SchemeName = "custom",
                SchemeHandlerFactory = new CustomSchemeHandlerFactory()
            });
            Cef.Initialize(settings);

            browser = new ChromiumWebBrowser();
            browser.MenuHandler = new CustomMenuHandler();
            browser.Address = "custom://index.html/";
            browser.RegisterAsyncJsObject("_bridge", new Bridge(browser), BindingOptions.DefaultBinder);

            Grid.SetRow(browser, 0);
            WebGrid.Children.Add(browser);
        }
    }

    public class CustomMenuHandler : CefSharp.IContextMenuHandler
    {
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
