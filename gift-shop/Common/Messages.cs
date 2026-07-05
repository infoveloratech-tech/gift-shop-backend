namespace gift_shop.Common;

public static class Messages
{
    // Success Messages
    public const string CreateSuccessMessage = "Created successfully";
    public const string UpdateSuccessMessage = "Updated successfully";
    public const string DeleteSuccessMessage = "Deleted successfully";
    public const string FetchSuccessMessage = "Fetched successfully";

    // Error Messages
    public const string InternalServerError = "Internal server error";
    public const string BadRequestError = "Bad request";
    public const string NotFoundError = "Resource not found";
    public const string UnauthorizedError = "Unauthorized access";
    public const string ForbiddenError = "Forbidden";
    public const string ConflictError = "Resource already exists";

    // Auth Messages
    public const string InvalidCredentialsError = "Invalid credentials";
    public const string UserAlreadyExistsError = "User already exists";
    public const string UserNotFoundError = "User not found";
    public const string InvalidTokenError = "Invalid token";
    public const string TokenExpiredError = "Token expired";

    // Validation Messages
    public const string RequiredFieldError = "This field is required";
    public const string InvalidEmailError = "Invalid email format";
    public const string PasswordTooWeakError = "Password is too weak";
    public const string InvalidPhoneError = "Invalid phone number";

    // Order Messages
    public const string OrderNotFoundError = "Order not found";
    public const string InvalidOrderStatusError = "Invalid order status";
    public const string InsufficientStockError = "Insufficient stock";

    // Product Messages
    public const string ProductNotFoundError = "Product not found";
    public const string CategoryNotFoundError = "Category not found";
    public const string SupplierNotFoundError = "Supplier not found";

    // Customer Messages
    public const string CustomerNotFoundError = "Customer not found";
    public const string InvalidCustomerError = "Invalid customer information";
}
