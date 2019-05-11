using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spartan.Common;

namespace Spartan.Extensions
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }

        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFromatter = new JsonSerializerSettings();
            camelCaseFromatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFromatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}