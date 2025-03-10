namespace DeepSeekUI.Application.Dtos
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public T? Value { get; }

        private Result(T value, bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
        }
        public static Result<T> Success(T value, string? message = null) => new(value, true, message);
        public static Result<T> Failure(string message) => new(default!, false, message);
    }

}
