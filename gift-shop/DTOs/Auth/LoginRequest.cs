namespace gift_shop.DTOs.Auth;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class GoogleLoginRequest
{
    public string IdToken { get; set; } = string.Empty;
}