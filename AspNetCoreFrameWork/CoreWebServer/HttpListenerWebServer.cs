using AspNetCoreFrameWork.CoreHttpContext;
using AspNetCoreFrameWork.CoreHttpFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreWebServer
{
    public class HttpListenerWebServer : IWebServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;
        public HttpListenerWebServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }
        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            Console.WriteLine("Server started and is listening on: {0}", string.Join(';', _urls));
            while (true)
            {
                var httpListenerContext = await _httpListener.GetContextAsync();
                var httpListenerFeature = new HttpListenerFeature(httpListenerContext);
                var featureCollection = new FeatureCollection()
                    .Set<IHttpRequestFeature>(httpListenerFeature)
                    .Set<IHttpResponseFeature>(httpListenerFeature);
                var httpContext = new HttpContext(featureCollection);
                await handler(httpContext);
                httpListenerContext.Response.Close();
            }
        }
    }
}
