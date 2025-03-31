namespace Application.Common.ResponseModels.Interfaces
{
    public interface IDataResponse<T> : IResponse
    {
        T Data { get; set; }
    }
}