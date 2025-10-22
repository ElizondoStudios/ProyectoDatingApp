namespace API.DTOs;

public class RegisterRequest
{
  public required string Email { get; set; } = string.Empty;
  public required string DisplayName { get; set; } = string.Empty;
  public required string Password { get; set; } = string.Empty;
}