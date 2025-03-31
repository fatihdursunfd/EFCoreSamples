using Application.Common.ResponseModels.Interfaces;
using Domain.Constants;

namespace Application.Common.ResponseModels.Models
{
    public class DataResponse<T> : Response, IDataResponse<T>
    {
        public T Data { get; set; }


        public DataResponse(T data, bool isSuccess, string messageCode, IEnumerable<string> messages) : base(isSuccess, messageCode, messages) 
            => Data = data;

        public DataResponse(T data, bool isSuccess, IEnumerable<string> messages) : base(isSuccess, MessageCodeConst.Success.OK, messages)
            => Data = data;

        public DataResponse(T data, bool isSuccess, string messageCode) : base(isSuccess, messageCode)
            => Data = data;

        public DataResponse(T data, bool isSuccess) : base(isSuccess, MessageCodeConst.Success.OK)
            => Data = data;
    }
}