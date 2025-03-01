﻿namespace ChatApp.Services;

public record class UserUpdateModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Avatar { get; set; }
    public string? Role { get; set; }
    public string? ConnectionId { get; set; }
}
