using Domain.Constants;

namespace Application.Common.ResponseModels.Models
{
    public class SuccessDataResponse<T> : DataResponse<T>
    {
        public SuccessDataResponse(T data, string messageCode, IEnumerable<string> message) : base(data, true, messageCode, message)
        {
        }

        public SuccessDataResponse(T data, IEnumerable<string> message) : base(data, true, MessageCodeConst.Success.OK, message)
        {
        }

        public SuccessDataResponse(T data) : base(data, true, MessageCodeConst.Success.OK)
        {
        }

        public SuccessDataResponse(T data, string messageCode) : base(data, true, messageCode)
        {
        }

    }
}