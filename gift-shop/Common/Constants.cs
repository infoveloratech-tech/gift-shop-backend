namespace gift_shop.Common;

public static class Constants
{
    public static class Pagination
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
    }

    public static class Messages
    {
        public const string SuccessMessage = "Operation successful";
        public const string ErrorMessage = "An error occurred";
        public const string NotFoundMessage = "Resource not found";
        public const string UnauthorizedMessage = "Unauthorized access";
        public const string BadRequestMessage = "Invalid request";
    }

    public static class Cache
    {
        public const string ProductsCacheKey = "products_cache";
        public const string CategoriesCacheKey = "categories_cache";
        public const string SuppliersCacheKey = "suppliers_cache";
        public const int CacheDurationMinutes = 30;
    }
}
