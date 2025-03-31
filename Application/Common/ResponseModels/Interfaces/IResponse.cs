namespace Application.Common.ResponseModels.Interfaces
{
    public interface IResponse
    {
        public bool IsSuccess { get; set; }

        public string MessageCode { get; set; }

        public string[] Messages { get; set; }
    }
}