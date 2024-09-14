namespace Randevucum.Authentication.Microservices.Basic.Domain.Primitives;

public class Result<T> : Result
{
    public T Data { get; }

    protected Result(T data, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data, true, string.Empty);
    }

    public new static Result<T?> Failure(string error)
    {
        return new Result<T?>(default, false, error);
    }
}
