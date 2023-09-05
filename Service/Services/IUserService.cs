using Domain.Requests;
using Domain.Response;

namespace Service.Services
{
    public interface IUserService
    {
        ICollection<UserResponse> GetAllUsers();
        Task<UserResponse> Register(CreateUserRequest createUserDto);
        Task<LoginUserResponse> Login(LoginUserRequest loginUserDto);
        bool IsUnique(string UserName);
    }
}
