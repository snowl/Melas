using CefSharp;

namespace Melas.Client
{
    public class CustomSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            return new CustomSchemeHandler();
        }
    }
}
