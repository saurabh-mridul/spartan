using System.Collections.Generic;

namespace Spartan.Interfaces
{
    public interface IResponse
    {
        string Message { get; set; }
        bool HasError { get; set; }
        string ErrorMessage { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Models { get; set; }
    }
}
