using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melas.Client
{
    public class ResourceHandlerFactory : IResourceHandlerFactory
    {
        public bool HasHandlers => true;

        public IResourceHandler GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            return new ResourceHandler();
        }
    }
}
