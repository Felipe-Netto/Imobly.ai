namespace ImoblyAI.Api.Common;

public class Result<T>
{
    public bool Success { get; }
    public string? Error { get; }
    public ErrorCode? ErrorCode { get; }
    public T? Data { get; }

    private Result(T data)
    {
        Success = true;
        Data = data;
    }

    private Result(string error, ErrorCode errorCode)
    {
        Success = false;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Result<T> Ok(T data) => new(data);

    public static Result<T> Fail(string error, ErrorCode errorCode) =>
        new(error, errorCode);
}