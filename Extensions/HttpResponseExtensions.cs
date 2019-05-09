using Microsoft.AspNetCore.Http;

namespace Spartan.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Eexpose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Region", "*");
        }
    }
}