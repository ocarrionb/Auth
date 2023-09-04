using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class LoginUserResponse
    {
        public User? User { get; set; }

        public string Token { get; set; }
    }
}
