namespace Authorization.Api.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string Roles { get; set; }
}
