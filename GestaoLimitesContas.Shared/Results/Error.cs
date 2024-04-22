namespace GestaoLimitesContas.Shared.Results
{
    public record Error(object error)
    {
        public static readonly Error None = new(string.Empty);

        public static implicit operator Result(Error error) => Result.Failure(error);

        public Result ToResult() => Result.Failure(this);
    }
}
