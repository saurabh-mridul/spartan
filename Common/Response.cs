using Spartan.Interfaces;
using System.Collections.Generic;

namespace Spartan.Common
{
    public class Response : IResponse
    {
        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public TModel Model { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<TModel> Models { get; set; }
    }
}
