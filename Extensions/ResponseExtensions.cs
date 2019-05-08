using Microsoft.AspNetCore.Mvc;
using Spartan.Interfaces;
using System.Linq;
using System.Net;

namespace Spartan.Extensions
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = response.HasError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

            return new ObjectResult(response) { StatusCode = (int)status };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.HasError)
                status = HttpStatusCode.InternalServerError;
            else if (response.Model == null)
                status = HttpStatusCode.NotFound;

            return new ObjectResult(response) { StatusCode = (int)status };
        }

        public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.HasError)
                status = HttpStatusCode.InternalServerError;
            else if (response.Models == null)
                status = HttpStatusCode.NotFound;
            else if (!response.Models.Any())
                status = HttpStatusCode.NoContent;

            return new ObjectResult(response) { StatusCode = (int)status };
        }
    }
}
