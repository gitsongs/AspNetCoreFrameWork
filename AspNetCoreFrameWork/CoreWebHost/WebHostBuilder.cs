using AspNetCoreFrameWork.CoreApplicationBuilder;
using AspNetCoreFrameWork.CoreWebServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFrameWork.CoreWebHost
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseWebServer(IWebServer server);
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        IWebHost Build();
    }
    public class WebHostBuilder : IWebHostBuilder
    {
        private IWebServer _server;
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }
        public IWebHostBuilder UseWebServer(IWebServer server)
        {
            _server = server;
            return this;
        }
        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            _configures.ForEach(x =>
            {
                x(builder);
            });
            return new WebHost(_server, builder.Build());
        }
    }
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseHttpListenerWebServer(this IWebHostBuilder builder, params string[] urls)
            => builder.UseWebServer(new HttpListenerWebServer(urls));
    }
}