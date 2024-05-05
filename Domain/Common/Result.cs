using System.Net;

namespace Domain.Common
{
    public abstract class BaseResult<B, T> where B : BaseResult<B, T>
    {
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        public BaseResult()
        { }

        public BaseResult(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public List<Error> Errors { get; set; } = [];
        public string Message { get; set; } = string.Empty;

        public bool HasErrors() => Errors.Count > 0;

        public HttpStatusCode GetStatusCode() => _statusCode;

        public void SetStatusCode(HttpStatusCode code)
        { _statusCode = code; }

        public B AddError(string message, string code = "")
        {
            Errors.Add(new Error(code, message));

            return (B)this;
        }
    }

    public class Result<T> : BaseResult<Result<T>, T>
    {
        public Result()
        { }

        public Result(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public T? Data { get; set; } = default;
    }
}