namespace Technic.Web.Models
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string? Message { get; set; }
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }

        internal Result(T value)
        {
            Value = value;
            Succeeded = true;
        }

        internal Result(string message)
        {
            Value = default(T);
            Message = message;
            Succeeded = false;
        }

        internal Result(string message, int statusCode)
        {
            Value = default(T);
            Message = message;
            Succeeded = false;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(string errors)
        {
            return new Result<T>(errors);
        }

        public static Result<T> Failure(string errors, int statusCode)
        {
            return new Result<T>(errors, statusCode);
        }
    }
}
