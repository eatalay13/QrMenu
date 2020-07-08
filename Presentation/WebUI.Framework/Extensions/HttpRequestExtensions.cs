using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Framework.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request == null
                ? throw new ArgumentNullException("request")
                : request.Headers != null && request.Headers[RequestedWithHeader] == XmlHttpRequest;
        }
    }
}
