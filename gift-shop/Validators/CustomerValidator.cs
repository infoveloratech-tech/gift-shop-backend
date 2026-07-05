using gift_shop.DTOs;
using System.Text.RegularExpressions;

namespace gift_shop.Validators;

public static class CustomerValidator
{
    /// <summary>
    /// Validates CreateCustomerDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateCreateCustomer(CreateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            return (false, "First name is required");

        if (dto.FirstName.Length < 2 || dto.FirstName.Length > 50)
            return (false, "First name must be between 2 and 50 characters");

        if (string.IsNullOrWhiteSpace(dto.LastName))
            return (false, "Last name is required");

        if (dto.LastName.Length < 2 || dto.LastName.Length > 50)
            return (false, "Last name must be between 2 and 50 characters");

        if (string.IsNullOrWhiteSpace(dto.Email))
            return (false, "Email is required");

        if (!IsValidEmail(dto.Email))
            return (false, "Invalid email format");

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && !IsValidPhoneNumber(dto.PhoneNumber))
            return (false, "Invalid phone number format");

        if (!string.IsNullOrWhiteSpace(dto.Address) && dto.Address.Length > 200)
            return (false, "Address cannot exceed 200 characters");

        if (!string.IsNullOrWhiteSpace(dto.City) && dto.City.Length > 50)
            return (false, "City cannot exceed 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.State) && dto.State.Length > 50)
            return (false, "State cannot exceed 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.PostalCode) && !IsValidPostalCode(dto.PostalCode))
            return (false, "Invalid postal code format");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates UpdateCustomerDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateUpdateCustomer(UpdateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            return (false, "First name is required");

        if (dto.FirstName.Length < 2 || dto.FirstName.Length > 50)
            return (false, "First name must be between 2 and 50 characters");

        if (string.IsNullOrWhiteSpace(dto.LastName))
            return (false, "Last name is required");

        if (dto.LastName.Length < 2 || dto.LastName.Length > 50)
            return (false, "Last name must be between 2 and 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && !IsValidPhoneNumber(dto.PhoneNumber))
            return (false, "Invalid phone number format");

        if (!string.IsNullOrWhiteSpace(dto.Address) && dto.Address.Length > 200)
            return (false, "Address cannot exceed 200 characters");

        if (!string.IsNullOrWhiteSpace(dto.City) && dto.City.Length > 50)
            return (false, "City cannot exceed 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.State) && dto.State.Length > 50)
            return (false, "State cannot exceed 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.PostalCode) && !IsValidPostalCode(dto.PostalCode))
            return (false, "Invalid postal code format");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates email format
    /// </summary>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email && email.Length <= 100;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates phone number format
    /// </summary>
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Remove common formatting characters
        var cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)\+\.]", "");

        // Check if it contains only digits and is between 10-15 characters
        return Regex.IsMatch(cleaned, @"^\d{10,15}$");
    }

    /// <summary>
    /// Validates postal code format
    /// </summary>
    public static bool IsValidPostalCode(string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        // Accept alphanumeric postal codes up to 20 characters
        return Regex.IsMatch(postalCode, @"^[A-Za-z0-9\s\-]{3,20}$");
    }

    /// <summary>
    /// Validates customer name
    /// </summary>
    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && 
               name.Length >= 2 && 
               name.Length <= 50 &&
               Regex.IsMatch(name, @"^[a-zA-Z\s'-]+$");
    }

    /// <summary>
    /// Validates full customer information
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateCustomerAddress(string address, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return (false, "Country is required");

        if (country.Length > 50)
            return (false, "Country cannot exceed 50 characters");

        return (true, string.Empty);
    }
}
