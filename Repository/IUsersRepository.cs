using Domain.Entity;
using Domain.Requests;
using Domain.Response;

namespace Repository
{
    public interface IUsersRepository
    {
        ICollection<User> GetAllUsers();

        User GetUser(int UserId);

        bool IsUniqueUser(string userName);

        Task<LoginUserResponse> Login(LoginUserRequest loginUserDto);

        Task<User> Register(CreateUserRequest createUserDto);
    }
}
