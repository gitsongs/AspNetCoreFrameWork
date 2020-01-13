using AspNetCoreFrameWork.CoreHttpContext;
using AspNetCoreFrameWork.CoreWebServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreWebHost
{
    public interface IWebHost
    {
        Task StartAsync();
    }
    public class WebHost : IWebHost
    {
        private readonly IWebServer _server;
        private readonly RequestDelegate _handler;
        public WebHost(IWebServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }
        public Task StartAsync() => _server.StartAsync(_handler);
    }
}
