using Domain.Constants;

namespace Application.Common.ResponseModels.Models
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string messageCode, IEnumerable<string> message) : base(false, messageCode, message)
        {
        }

        public ErrorResponse(IEnumerable<string> message) : base(false, MessageCodeConst.Error.NotOK, message)
        {
        }

        public ErrorResponse() : base(false, MessageCodeConst.Error.NotOK)
        {
        }

        public static Response Failure(IEnumerable<string> errors)
        {
            return new ErrorResponse(errors);
        }
    }
}