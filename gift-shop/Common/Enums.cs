namespace gift_shop.Common;

public enum StatusEnum
{
    Active = 1,
    Inactive = 0,
    Deleted = -1
}

public enum OrderStatusEnum
{
    Pending = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Returned = 6
}

public enum PaymentStatusEnum
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4
}

public enum UserRoleEnum
{
    Admin = 1,
    Manager = 2,
    Employee = 3,
    Customer = 4
}
