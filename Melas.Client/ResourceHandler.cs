using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Melas.Client
{
    public class ResourceHandler : IResourceHandler
    {
        private String File { get; set; }

        private Stream Stream { get; set; }

        public void Cancel()
        {

        }

        public bool CanGetCookie(Cookie cookie)
        {
            return true;
        }

        public bool CanSetCookie(Cookie cookie)
        {
            return true;
        }

        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }
        }

        public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
        {
            var assembly = Assembly.GetExecutingAssembly();
            this.Stream = assembly.GetManifestResourceStream("Melas.Client.Resources." + File);
            
            if (this.Stream == null)
            {
                responseLength = 0;
                redirectUrl = null;
                response.ErrorCode = CefErrorCode.FileNotFound;
            }
            else
            {
                responseLength = this.Stream.Length;
                redirectUrl = null;
            }
        }

        public bool ProcessRequest(IRequest request, ICallback callback)
        {
            this.File = request.Url.Replace("http://", "");
            this.File = this.File.Substring(0, this.File.Length - 1);
            callback.Continue();
            return true;
        }

        public bool ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
        {
            callback.Dispose();

            if (Stream == null)
            {
                bytesRead = 0;
                return false;
            }
            
            var buffer = new byte[dataOut.Length];
            bytesRead = Stream.Read(buffer, 0, buffer.Length);

            dataOut.Write(buffer, 0, buffer.Length);

            return bytesRead > 0;
        }
    }
}
