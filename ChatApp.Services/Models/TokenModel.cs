namespace ChatApp.Services;

public class TokenModel
{
    public string UserName { get; set; }
    public string Role { get; set; }
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
