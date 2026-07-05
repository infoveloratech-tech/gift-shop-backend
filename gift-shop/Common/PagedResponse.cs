namespace gift_shop.Common;

public class PagedResponse<T> : ApiResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }

    public PagedResponse()
    {
    }

    public PagedResponse(bool success, string message, T data, int pageNumber, int pageSize, int totalCount, int statusCode = 200)
        : base(success, message, data, statusCode)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
