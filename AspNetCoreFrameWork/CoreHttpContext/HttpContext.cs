using AspNetCoreFrameWork.CoreHttpFeature;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreHttpContext
{
    public class HttpContext
    {
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }
        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }
    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;
        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
        public Uri Url => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
    }
    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;
        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
        public async Task WriteAsync(string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            await this.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
