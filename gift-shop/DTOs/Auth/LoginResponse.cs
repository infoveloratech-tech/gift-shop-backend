namespace gift_shop.DTOs.Auth;

public class LoginResponse
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class UserProfileDto
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? ProfileImage { get; set; }
}

public class ApiResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}
