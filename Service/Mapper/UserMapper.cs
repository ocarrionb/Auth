using AutoMapper;
using Domain.Entity;
using Domain.Requests;
using Domain.Response;

namespace Service.Mapper
{
    public sealed class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>();
            CreateMap<LoginUser, LoginUserResponse>();
            CreateMap<CreateUserRequest, User>();
        }
    }
}
