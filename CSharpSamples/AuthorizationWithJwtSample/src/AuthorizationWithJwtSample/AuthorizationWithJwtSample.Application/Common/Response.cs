namespace AuthorizationWithJwtSample.Application.Common;

public class Response<T>
{
    public string? Message { get; init; }
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }

    public Response(bool isSuccess, string message, T? data)
    {
        Message= message;
        IsSuccess = isSuccess;
        Data = data;
    }
}
