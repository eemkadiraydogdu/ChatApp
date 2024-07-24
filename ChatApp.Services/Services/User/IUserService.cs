using ChatApp.Data;

namespace ChatApp.Services;

public interface IUserService :IGenericService<User>
{
    Task<User> GetByUsernameAndPasswordAsync(string username, string password);
    Task<List<User>> GetUsersByNameAsync(string name);
}
