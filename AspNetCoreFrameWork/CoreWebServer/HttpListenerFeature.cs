using AspNetCoreFrameWork.CoreHttpFeature;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace AspNetCoreFrameWork.CoreWebServer
{
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _context;
        public HttpListenerFeature(HttpListenerContext context) => _context = context;
        #region//IHttpRequestFeature
        Uri IHttpRequestFeature.Url => _context.Request.Url;
        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;
        Stream IHttpRequestFeature.Body => _context.Request.InputStream;
        #endregion
        #region//IHttpResponseFeature
        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;
        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;
        int IHttpResponseFeature.StatusCode
        {
            get => _context.Response.StatusCode;
            set => _context.Response.StatusCode = value;
        }
        #endregion
    }
}
