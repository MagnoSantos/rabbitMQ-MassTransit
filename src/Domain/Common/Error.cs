namespace Domain.Common
{
    public class Error
    {
        public string Message { get; init; }
        public string Code { get; init; } = string.Empty;
        public string PropertyName { get; init; } = string.Empty;

        public Error(string message)
        {
            Message = message;
        }
        public Error(string code, string message)
        {
            Message = message;
            Code = code;
        }

        public Error(string propertyName, string code, string message)
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
        }
    }
}