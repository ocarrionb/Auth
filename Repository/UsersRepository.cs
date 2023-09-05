using Domain;
using Domain.Entity;
using Domain.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(int UserId)
        {
            return _context.User.FirstOrDefault(u => u.Id == UserId);
        }

        public bool IsUniqueUser(string userName)
        {
            var usuarioBd = _context.User.FirstOrDefault(u => u.UserName == userName);
            if (usuarioBd == null)
            {
                return true;
            }
            return false;
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.User.OrderBy(u => u.Name).ToList();
        }

        public async Task<LoginUser> Login(LoginUserRequest loginUserRequest, string SecretKey)
        {
            var user = _context.User.FirstOrDefault(
                u => u.UserName == loginUserRequest.UserName &&
                u.Password == loginUserRequest.Password);

            if (user == null)
            {
                LoginUser? userSession = new LoginUser()
                {
                    Token = "",
                    User = null
                };
                return userSession;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);

                LoginUser loginUser = new LoginUser()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                };
                return loginUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error into Repository - Login: {ex}");
                throw;
            }
        }

        public async Task<User> Register(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
