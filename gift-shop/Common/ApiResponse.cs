namespace gift_shop.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(bool success, string message, T data, int statusCode = 200)
    {
        Success = success;
        Message = message;
        Data = data;
        StatusCode = statusCode;
    }
}
