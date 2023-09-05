using Domain.Entity;
using Domain.Options;
using Domain.Requests;

namespace Repository
{
    public interface IUsersRepository
    {
        ICollection<User> GetAllUsers();

        User GetUser(int UserId);

        bool IsUniqueUser(string userName);

        Task<LoginUser> Login(LoginUserRequest loginUserDto, string SecretKey);

        Task<User> Register(User createUserDto);
    }
}
