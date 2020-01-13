using AspNetCoreFrameWork.CoreHttpContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreApplicationBuilder
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        RequestDelegate Build();
    }
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();
        public RequestDelegate Build()
        {
            _middlewares.Reverse();
            return httpContext =>
            {
                RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
                _middlewares.ForEach(x =>
                {
                    next = x(next);
                });
                return next(httpContext);
            };
        }
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
