namespace gift_shop.Helpers;

public class PaginationHelper
{
    /// <summary>
    /// Calculates the number of pages needed
    /// </summary>
    public static int GetTotalPages(int totalCount, int pageSize)
    {
        if (pageSize <= 0) return 0;
        return (int)Math.Ceiling((double)totalCount / pageSize);
    }

    /// <summary>
    /// Validates page and page size
    /// </summary>
    public static (int page, int pageSize) ValidatePageParameters(int page, int pageSize, int maxPageSize = 100)
    {
        var validPage = page < 1 ? 1 : page;
        var validPageSize = pageSize < 1 ? 10 : pageSize > maxPageSize ? maxPageSize : pageSize;
        return (validPage, validPageSize);
    }

    /// <summary>
    /// Calculates skip count for database queries
    /// </summary>
    public static int GetSkipCount(int page, int pageSize)
    {
        return (page - 1) * pageSize;
    }

    /// <summary>
    /// Creates a pagination response
    /// </summary>
    public static PaginationResponse<T> CreatePaginationResponse<T>(
        List<T> items, 
        int page, 
        int pageSize, 
        int totalCount)
    {
        var totalPages = GetTotalPages(totalCount, pageSize);
        var hasNextPage = page < totalPages;
        var hasPreviousPage = page > 1;

        return new PaginationResponse<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPage = hasNextPage,
            HasPreviousPage = hasPreviousPage
        };
    }

    /// <summary>
    /// Apply pagination to IEnumerable
    /// </summary>
    public static IEnumerable<T> ApplyPagination<T>(IEnumerable<T> source, int page, int pageSize)
    {
        var (validPage, validPageSize) = ValidatePageParameters(page, pageSize);
        return source.Skip(GetSkipCount(validPage, validPageSize)).Take(validPageSize);
    }
}

/// <summary>
/// Pagination response model
/// </summary>
public class PaginationResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}
