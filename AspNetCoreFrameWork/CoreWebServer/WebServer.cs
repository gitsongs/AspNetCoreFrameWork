using AspNetCoreFrameWork.CoreHttpContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreWebServer
{
    public interface IWebServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
