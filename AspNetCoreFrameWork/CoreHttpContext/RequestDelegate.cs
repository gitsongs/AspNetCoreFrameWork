using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreFrameWork.CoreHttpContext
{
    public delegate Task RequestDelegate(HttpContext context);
}
