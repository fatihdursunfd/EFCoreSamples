using Domain.Constants;

namespace Application.Common.ResponseModels.Models
{
    public class ErrorDataResponse<T> : DataResponse<T>
    {
        public ErrorDataResponse(T data, string messageCode, IEnumerable<string> message) : base(data, false, messageCode, message)
        {
        }

        public ErrorDataResponse(T data, IEnumerable<string> message) : base(data, false, MessageCodeConst.Error.NotOK, message)
        {
        }

        public ErrorDataResponse(T data) : base(data, false, MessageCodeConst.Error.NotOK)
        {
        }

        public ErrorDataResponse(T data, string messageCode) : base(data, false, messageCode)
        {
        }

        public ErrorDataResponse(IEnumerable<string> message) : base(default!, false, MessageCodeConst.Error.NotOK, message)
        {
        }

        public ErrorDataResponse(string messageCode, IEnumerable<string> message) : base(default!, false, messageCode, message)
        {
        }

        public ErrorDataResponse() : base(default!, false, MessageCodeConst.Error.NotOK)
        {
        }

    }
}