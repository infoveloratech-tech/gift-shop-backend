namespace gift_shop.DTOs.Auth;

public class RegisterRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }
}

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}