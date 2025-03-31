using Domain.Constants;

namespace Application.Common.ResponseModels.Models
{
    public class SuccessResponse : Response
    {
        public SuccessResponse(bool isSuccess, string messageCode) : base(isSuccess, messageCode)
        {
        }

        public SuccessResponse(IEnumerable<string> messages) : base(true, MessageCodeConst.Success.OK, messages)
        {
        }

        public SuccessResponse(string messageCode, IEnumerable<string> messages) : base(true, messageCode, messages)
        {
        }

        public SuccessResponse(string messageCode) : base(true, messageCode)
        {
        }

        public SuccessResponse() : base(true, MessageCodeConst.Success.OK)
        {
        }

        public SuccessResponse(bool success, string messageCode, IEnumerable<string> message, object? data = null) : base(true, MessageCodeConst.Success.OK, message)
        {
        }

        public static Response Success()
        {
            return new SuccessResponse();
        }
    }
}