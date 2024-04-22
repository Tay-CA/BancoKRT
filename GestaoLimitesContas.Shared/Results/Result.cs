namespace GestaoLimitesContas.Shared.Results
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error, object data)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public object Data { get; }

        public static Result Success() => new(true, null, null);

        public static Result Success(object success) => new(true, null, success);

        public static Result Failure(object error) => new(false, new(error), null);
    }
}
