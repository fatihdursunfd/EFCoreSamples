using Application.Common.ResponseModels.Interfaces;

namespace Application.Common.ResponseModels.Models
{
    public class Response : IResponse
    {
        public bool IsSuccess { get; set; }
        public string MessageCode { get; set; } = string.Empty;
        public string[] Messages { get; set; } = Array.Empty<string>();

        public Response(bool isSuccess, string messageCode)
        {
            IsSuccess = isSuccess;
            MessageCode = messageCode;
        }

        public Response(bool isSuccess, string messageCode, IEnumerable<string> messages) : this(isSuccess, messageCode)
        {
            IsSuccess = isSuccess;
            MessageCode = messageCode;
            Messages = messages.ToArray();
        }
    }
}   