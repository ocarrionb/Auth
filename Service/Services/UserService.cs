using Repository;
using AutoMapper;
using XSystem.Security.Cryptography;
using Domain.Response;
using Domain.Requests;
using Domain.Entity;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Service.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly SecretOptions _secretOptions;

        public UserService(IUsersRepository usersRepository, 
            IMapper mapper,
            IOptions<SecretOptions> secretOptions)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _secretOptions = secretOptions.Value;
        }

        public bool IsUnique(string UserName)
        {
            return _usersRepository.IsUniqueUser(UserName);
        }

        public ICollection<UserResponse> GetAllUsers()
        {
            var listUsers = _usersRepository.GetAllUsers();
            var listUsersResponse = new List<UserResponse>();

            foreach (var item in listUsers)
            {
                listUsersResponse.Add(_mapper.Map<UserResponse>(item));
            }
            return listUsersResponse;
        }

        public async Task<UserResponse> Register(CreateUserRequest createUserRequest)
        {
            var passwordEncriptado = GetMd5(createUserRequest.Password);
            createUserRequest.Password = passwordEncriptado;
            var createUser = _mapper.Map<User>(createUserRequest);
            var user = await _usersRepository.Register(createUser);
            var useresponse = _mapper.Map<UserResponse>(user);
            return useresponse;
        }

        public async Task<LoginUserResponse> Login(LoginUserRequest loginUserRequest)
        {
            var passwordEncriptado = GetMd5(loginUserRequest.Password);
            loginUserRequest.Password = passwordEncriptado;
            var user = await _usersRepository.Login(loginUserRequest, _secretOptions.SecretKey);
            var userResponse = _mapper.Map<LoginUserResponse>(user);
            return userResponse;
        }

        private static string GetMd5(string pass)
        {
            MD5CryptoServiceProvider tmp = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(pass);
            data = tmp.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
