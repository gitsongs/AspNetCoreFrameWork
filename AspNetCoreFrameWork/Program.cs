using AspNetCoreFrameWork.CoreHttpContext;
using AspNetCoreFrameWork.CoreWebHost;
using System;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await new WebHostBuilder()
                .UseHttpListenerWebServer()
                .Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware))
                .Build()
                .StartAsync();
        }
        public static RequestDelegate FooMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Foo=>");
                await next(context);
            };

        public static RequestDelegate BarMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Bar=>");
                await next(context);
            };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Baz");
                //await next(context);
            };
    }
}